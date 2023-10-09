import {
  ChangeDetectorRef,
  Component,
  forwardRef,
  Inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { ClipboardService } from 'ngx-clipboard';
import { TreeNode } from 'primeng/api';
import { Subject, Subscription } from 'rxjs';
import { ContextMenuItem } from '../../../../models/Trees/ContextMenuItem';
import { BillingService } from '../../../../services/Billing/billing.service';
import { AppComponent } from '../../../app.component';
import { SharedService } from '../../../services/shared.service';
import * as XLSX from 'xlsx';
import { faArrowAltCircleLeft, faListDots } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

class ItemStructure {
  itemStructureCode?: string;
  itemStructureDesFrn: string;
  itemStructureDesNtv: string;
  structureGeneralDetailFlag: boolean;
  structureLevelNum: number;
  parentStructure: string;
  defaultIssueAccountCode: string;
}

@Component({
  selector: 'app-billing',
  templateUrl: './billing.component.html',
  styleUrls: ['./billing.component.css'],
})
export class BillingComponent implements OnInit {
  // icons
  faListDots = faListDots;

  langServiceSupscribtion: Subscription | undefined;
  faArrowAltCircleLeft = faArrowAltCircleLeft;

  // Tree
  data: TreeNode[];
  lang: any = 'en';
  contextMenuitems: ContextMenuItem[];
  selectedNode: TreeNode;
  selectedNodeCode: string;
  columns: any[];
  // Datatable
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  dtInstanceObj: any;
  initialized: boolean = false;

  services: any[] = [];

  edit: boolean = false;

  // Modal Variables
  displayModal: boolean = false;

  // Add / Edit Form
  addEditItemForm = this.fb.group({
    itemStructureCode: [null],
    itemStructureDesFrn: ['', Validators.required],
    itemStructureDesNtv: ['', Validators.required],
    structureGeneralDetailFlag: [true],
    structureLevelNum: [0, Validators.required],
    parentStructure: ['', Validators.required],
    defaultIssueAccountCode: ['', Validators.required],
  });

  constructor(
    private billingService: BillingService,
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private sharedService: SharedService,
    private fb: FormBuilder,
    private translateService: TranslateService,
    private clipboardApi: ClipboardService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
      this.contextMenuitems = [
        {
          label: this.lang == 'en' ? 'Add Sibling' : 'إضافة على نفس المستوى',
          icon: 'pi pi-plus',
          command: (event) =>
            this.showModalDialog(this.selectedNode.data, 'add', 'sibling'),
        },
        {
          label: this.lang == 'en' ? 'Add Child' : 'أضافة على المستوى التالي',
          icon: 'pi pi-plus',
          command: (event) =>
            this.showModalDialog(this.selectedNode.data, 'add', 'child'),
        },
        {
          label: this.lang == 'en' ? 'Edit' : 'تعديل',
          icon: 'pi pi-pencil',
          command: (event) =>
            this.showModalDialog(this.selectedNode.data, 'edit', 'child'),
        },
        {
          label: this.lang == 'en' ? 'Delete' : 'حذف',
          icon: 'pi pi-trash',
          command: (event) => this.deleteItemStructure(),
        },
      ];
    });
    this.fillTree();
    this.getServices();
  }

  ngAfterViewInit(): void {
    var me = this;
    this.dtTrigger.subscribe(() => {
      this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
        if (!this.initialized) {
          this.initialized = true;
          me.dtInstanceObj = dtInstance;
          dtInstance.columns().every(function (e) {
            const that = this;
            var counter = -1;
            $('.search-input').each(function () {
              counter += 1;
              if (counter == e) {
                $(this).on('keyup change', function () {
                  var value = $(this).val();
                  if (that.search() !== value) {
                    that.search(value as string).draw();
                  }
                });
              }
            });
          });
        }
      });
    });
  }

  fillTree() {
    this.billingService.GetAll().subscribe((response) => {
      this.data = <TreeNode[]>response;
      console.log('Tree Nodes: ', this.data);
      this.columns = [
        { field: 'itemStructureCode', header: 'Code' },
        { field: 'itemStructureDesFrn', header: 'Name Eng' },
        { field: 'itemStructureDesNtv', header: 'Name Ar' },
        { field: 'defaultIssueAccountCode', header: 'Account Code' },

      ];
     
    });

  }

  // Get Services
  getServices() {
    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: true,
      serverSide: true,
      searching: true,
      ajax: (dataTablesParameters: any, callback, settings) => {
        console.log(dataTablesParameters);
        dataTablesParameters.searchCriteria = this._searchCriteria;
        dataTablesParameters.extra = this.selectedNodeCode;
        this.billingService
          .GetServicesByItemCode(dataTablesParameters)
          .subscribe((resp) => {
            this.services = resp['data'];
            console.log('Services: ', this.services);
            callback({
              recordsTotal: resp['recordsTotal'],
              recordsFiltered: resp['recordsFiltered'],
              data: [],
            });
          });
      },
      columns: [
        { data: 'itemRefCode' },
        { data: 'itemDesFrn' },
        { data: 'itemDesNtv' },
        { data: 'itemType' },
        { data: 'issueAccountCode' },
        { data: 'currentPrice' },
        { data: null },
      ],
    };
  }

  // Context Menu Methods
  viewNode(node) {
    this.app.messageService.add({
      severity: 'info',
      summary: 'File Selected',
      detail:
        node.data.itemStructureCode + ' - ' + node.data.itemStructureDesFrn,
    });
  }

  toggleNode(node) {
    node.expanded = !node.expanded;
    this.data = [...this.data];
  }

  // Modal
  showModalDialog(node, operation, target?) {
    console.log('node', node);
    let body = <ItemStructure>node;
    this.displayModal = true;

    if (operation == 'add') {
      if (target == 'sibling') {
        console.log(node);
        this.addEditItemForm.controls['parentStructure'].setValue(
          body.parentStructure
        );
        this.addEditItemForm.controls['structureLevelNum'].setValue(
          body.structureLevelNum
        );
      } else if (target == 'child') {
        this.addEditItemForm.controls['parentStructure'].setValue(
          body.itemStructureCode
        );
        this.addEditItemForm.controls['structureLevelNum'].setValue(
          body.structureLevelNum + 1
        );
      }
    } else if (operation == 'edit') {
      this.edit = true;
      this.addEditItemForm.controls['defaultIssueAccountCode'].setValue(
        body.defaultIssueAccountCode
      );
      this.addEditItemForm.controls['structureLevelNum'].setValue(
        body.structureLevelNum
      );
      this.addEditItemForm.controls['itemStructureCode'].setValue(
        body.itemStructureCode
      );
      this.addEditItemForm.controls['itemStructureDesFrn'].setValue(
        body.itemStructureDesFrn
      );
      this.addEditItemForm.controls['itemStructureDesNtv'].setValue(
        body.itemStructureDesNtv
      );
      this.addEditItemForm.controls['structureGeneralDetailFlag'].setValue(
        body.structureGeneralDetailFlag
      );
    }
  }

  closeModal() {
    this.displayModal = false;
    this.addEditItemForm.reset();
    this.addEditItemForm.controls['structureGeneralDetailFlag'].setValue(true);
  }

  // Tree Mehtods
  expandDeepChild(data: any) {
    let child = data;
    for (let i = 0; i < child.length; i++) {
      if (child[i].children.length > 0) {
        this.expandDeepChild(child[i].children);
        child[i].expanded = true;
      }
    }
  }

  expandAll() {
    this.expandDeepChild(this.data);
    this.data = [...this.data];
  }

  collapseDeepChild(data: any) {
    let child = data;
    for (let i = 0; i < child.length; i++) {
      if (child[i].children.length > 0) {
        this.expandDeepChild(child[i].children);
        child[i].expanded = false;
      }
    }
  }

  collapseAll() {
    this.collapseDeepChild(this.data);
    this.data = [...this.data];
  }

  addItemStructure() {
    let body = <ItemStructure>this.addEditItemForm.value;
    this.billingService.Add(body).subscribe((response) => {
      if (response['succeeded']) {
        this.fillTree();
        this.app.messageService.add({
          severity: 'success',
          key: 'PlanValidation',
          summary: this.translateService.instant('General.SavedSuccessfully'),
          detail: this.translateService.instant('General.SavedSuccessfully'),
          life: 6000,
        });
      } else {
        this.app.messageService.add({
          severity: 'error',
          key: 'PlanValidation',
          summary: this.translateService.instant('General.Error'),
          detail:
            this.translateService.instant('General.Error') +
            ' ' +
            response['errors'].join('=>'),
          life: 6000,
        });
      }
      this.displayModal = false;
    });
    this.addEditItemForm.reset();
    this.addEditItemForm.controls['structureGeneralDetailFlag'].setValue(true);
  }

  deleteItemStructure() {
    let code = this.selectedNode.data.itemStructureCode;
    this.app.confirmationService.confirm({
      message:
        this.translateService.instant('Confirmation.deleteMsg') + ': ' + code,
      header: this.translateService.instant('Confirmation.confirm'),
      acceptLabel: this.translateService.instant('Confirmation.Yes'),
      rejectLabel: this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.billingService.Delete(code).subscribe((response) => {
          console.log(response);
          if (response['succeeded']) {
            this.fillTree();
            this.app.messageService.add({
              severity: 'success',
              key: 'PlanValidation',
              summary: this.translateService.instant(
                'General.DeletedSuccessfully'
              ),
              detail: this.translateService.instant(
                'General.DeletedSuccessfully'
              ),
              life: 6000,
            });
          } else {
            this.app.messageService.add({
              severity: 'error',
              key: 'PlanValidation',
              summary: this.translateService.instant('General.Error'),
              detail: response['message'],
              life: 6000,
            });
          }
        });
      },
      reject: () => {
        this.app.messageService.add({
          severity: 'warn',
          key: 'PlanValidation',
          summary: this.translateService.instant('General.DeleteCancelled'),
          detail: this.translateService.instant('General.DeleteCancelled'),
          life: 6000,
        });
      },
    });
  }

  editItemStructure() {
    let body = <ItemStructure>this.addEditItemForm.value;
    this.billingService.Update(body).subscribe((response) => {
      console.log(response);
      if (response['succeeded']) {
        this.fillTree();
        this.app.messageService.add({
          severity: 'success',
          key: 'PlanValidation',
          summary: this.translateService.instant('General.SavedSuccessfully'),
          detail: this.translateService.instant('General.SavedSuccessfully'),
          life: 6000,
        });
      } else {
        this.app.messageService.add({
          severity: 'error',
          key: 'PlanValidation',
          summary: this.translateService.instant('General.Error'),
          detail:
            this.translateService.instant('General.Error') +
            ' ' +
            response['errors'].join('=>'),
          life: 6000,
        });
      }
      this.displayModal = false;
    });
    this.addEditItemForm.reset();
    this.addEditItemForm.controls['structureGeneralDetailFlag'].setValue(true);
  }

  nodeSelect(event) {
    if (event.node.children.length==0) {
      this.selectedNodeCode = event.node.data.itemStructureCode;
      this.getServices();
      this.rerender();
    }
  }

  Back() {
    this.router.navigate(['/'])
  }
  // Datatable Methods
  reload() {
    var _this = this;
    _this.getServices();
    _this.rerender();
  }

  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('billingServicesDatatable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, 'services.xlsx');
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('General.SuccessExportExcel'),
      detail: this.translateService.instant('General.SuccessExportExcel'),
      life: 6000,
    });
  }
  Print() {
    let element = document.getElementById('billingServicesDatatable');
    let newWin = window.open('');
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('billingServicesDatatable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('General.SuccessCopy'),
      detail: this.translateService.instant('General.SuccessCopy'),
      life: 6000,
    });
  }
  // DataTable Methods
  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
      dtInstance.ajax.reload();
    });
  }

  search() {
    this.rerender();
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  private refreshData(): void {
    this.rerender();
    this.subscribeToData();
  }

  private subscribeToData(): void {
    this.refreshData();
  }
}
