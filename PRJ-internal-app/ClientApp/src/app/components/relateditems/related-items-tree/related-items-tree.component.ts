import { FormBuilder, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { ClipboardService } from 'ngx-clipboard';
import { TreeNode } from 'primeng/api';
import { Subject, Subscription } from 'rxjs';
import { ContextMenuItem } from '../../../../models/Trees/ContextMenuItem';
import { AppComponent } from '../../../app.component';
import { SharedService } from '../../../services/shared.service';
import * as XLSX from 'xlsx';
import { RelatedItemsService } from '../../../../services/RelatedItems/relatedItems.service';
import {
  faTrash,
  faPenToSquare,
  faArrowAltCircleLeft,
  faEye,
} from '@fortawesome/free-solid-svg-icons';
import {
  Component,
  forwardRef,
  Inject,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';

class ItemStructure {
  itemStructureCode?: string;
  itemStructureDesFrn: string;
  itemStructureDesNtv: string;
  structureGeneralDetailFlag: number;
  structureLevelNum: number;
  parentStructure: string;
  itemStructureLongDes: string;
}

@Component({
  selector: 'app-related-items-tree',
  templateUrl: './related-items-tree.component.html',
  styleUrls: ['./related-items-tree.component.css'],
})
export class RelatedItemsTreeComponent implements OnInit {
  langServiceSupscribtion: Subscription | undefined;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  faEye = faEye;
  // Tree
  data: TreeNode[];
  lang: any = 'en';
  contextMenuitems: ContextMenuItem[];
  selectedNode: TreeNode;
  cols: any[];
  columns: any[];

  // Datatable
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  dtInstanceObj: any;
  initialized: boolean = false;
  RelatedItems: any[] = [];
  services: any[] = [];
  structureCode: string = '';
  edit: boolean = false;
  itemstrc = '';
  // Modal Variables

  displayModal: boolean = false;
  olddata = {
    itemStructureDesFrn: '',
    itemStructureDesNtv: '',
    ItemStructureLongDes: '',
  };
  // Add / Edit Form
  addEditItemForm = this.fb.group({
    ItemStructureCode: [null],
    ItemStructureDesFrn: ['', Validators.required],
    ItemStructureDesNtv: ['', Validators.required],
    StructureGeneralDetailFlag: [0],
    StructureLevelNum: [0],
    ParentStructure: [''],
    ItemStructureLongDes: [''],
  });

  constructor(
    private relateditemsService: RelatedItemsService,
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private sharedService: SharedService,
    private fb: FormBuilder,
    private translateService: TranslateService,
    private clipboardApi: ClipboardService,
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
    this.getServices();
    this.fillTree();
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
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }

  Back() {
    this.router.navigate(['/related-items']);
  }
  fillTree() {
    this.relateditemsService
      .GetAllHierarchy(this.itemstrc)
      .subscribe((response) => {
        this.data = <TreeNode[]>response;
        this.columns = [
          { field: 'itemStructureCode', header: 'itemStructureCode' },
          { field: 'itemStructureDesFrn', header: 'itemStructureDesFrn' },
        ];
        console.log('Tree Nodes: ', this.data);
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
        this.relateditemsService
          .GetAllFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log('resp: ', resp);
              this.dtTrigger.next();
              this.RelatedItems = resp['data'];
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: resp['data'],
              });
            } else {
              this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: this.translateService.instant('General.Error'),
                detail:
                  this.translateService.instant('General.Error') +
                  ' ' +
                  resp['errors'].join('=>'),
                life: 6000,
              });
            }
          });
      },
      columns: [
        { data: 'manufacturerSerialNom' },
        { data: 'hierarchyStructureCode' },
        { data: 'hsCode' },
        { data: 'description' },
      ],
    };
    // this.dtOptions = {
    //   paging: true,
    //   pagingType: 'full_numbers',
    //   pageLength: 10,
    //   autoWidth: true,
    //   processing: true,
    //   serverSide: true,
    //   searching: true,
    //   ajax: (dataTablesParameters: any, callback, settings) => {
    //     dataTablesParameters.searchCriteria = this._searchCriteria;
    //     dataTablesParameters.extra = this.structureCode;
    //     this.relateditemsService.GetAllFunctional(
    //       dataTablesParameters
    //     ).subscribe((resp) => {
    //       if (resp['succeeded']) {
    //         this.dtTrigger.next();
    //         this.RelatedItems = resp['data'];
    //         console.log(resp['data']);
    //         callback({
    //           recordsTotal: resp['recordsTotal'],
    //           recordsFiltered: resp['recordsFiltered'],
    //           data: resp['data'],
    //         });
    //       }
    //     });
    //   },
    //   columns: [
    //     {
    //       title: this.translateService.instant('hsCode'),
    //       data: 'hsCode',
    //       class: 'sortingw1',
    //     },
    //     {
    //       title: this.translateService.instant('manufacturerSerialNom'),
    //       data: 'manufacturerSerialNom',
    //     },
    //     {
    //       title: this.translateService.instant('description'),
    //       data: 'description',
    //     },
    //     { title: this.translateService.instant('Purpose'), data: 'purpose' },
    //     {
    //       title: this.translateService.instant('permitinitialQty'),
    //       data: 'permitinitialQty',
    //     },
    //     {
    //       title: this.translateService.instant('ItemStr'),
    //       data: 'itemStructureCode',
    //       class: 'none',
    //     },
    //     {
    //       title: this.translateService.instant('itemTypeName'),
    //       data: 'itemTypeName',
    //       class: 'none',
    //     },
    //     {
    //       title: this.translateService.instant('itemTypeNumber'),
    //       data: 'itemTypeNumber',
    //       class: 'none',
    //     },
    //     {
    //       title: this.translateService.instant('dateOfManufacturing'),
    //       data: 'dateOfManufacturing',
    //       class: 'none',
    //     },
    //     {
    //       title: this.translateService.instant('relatedItemCode'),
    //       data: 'relatedItemCode',
    //       class: 'none',
    //     },
    //   ],
    //   responsive: true,
    // };
  }

  viewdetailsNode(x) {
    var newstring = x.trim();
    this.structureCode = newstring;
    this.reload();
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
      this.edit = false;
      if (target == 'sibling') {
        console.log(node);
        this.addEditItemForm.controls['ParentStructure'].setValue(
          body.parentStructure.trim()
        );
        this.addEditItemForm.controls['StructureLevelNum'].setValue(
          body.structureLevelNum
        );
      } else if (target == 'child') {
        this.addEditItemForm.controls['ParentStructure'].setValue(
          body.itemStructureCode.trim()
        );
        this.addEditItemForm.controls['StructureLevelNum'].setValue(
          body.structureLevelNum + 1
        );
      }
    } else if (operation == 'edit') {
      this.edit = true;
      this.olddata.itemStructureDesFrn = body.itemStructureDesFrn;
      this.olddata.itemStructureDesNtv = body.itemStructureDesNtv;
      this.olddata.ItemStructureLongDes = body.itemStructureLongDes;
      this.addEditItemForm.controls['ItemStructureCode'].setValue(
        body.itemStructureCode.trim()
      );
      this.addEditItemForm.controls['structureLevelNum'].setValue(
        body.structureLevelNum
      );
      this.addEditItemForm.controls['ItemStructureLongDes'].setValue(
        body.itemStructureLongDes
      );
      this.addEditItemForm.controls['ItemStructureDesFrn'].setValue(
        body.itemStructureDesFrn.trim()
      );
      this.addEditItemForm.controls['ItemStructureDesNtv'].setValue(
        body.itemStructureDesNtv.trim()
      );
      this.addEditItemForm.controls['StructureGeneralDetailFlag'].setValue(
        body.structureGeneralDetailFlag
      );
    }
  }

  closeModal() {
    this.displayModal = false;
    this.addEditItemForm.reset();
    this.addEditItemForm.controls['StructureGeneralDetailFlag'].setValue(1);
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
    console.log(body);
    this.relateditemsService.AddHierarchy(body).subscribe((response) => {
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
    this.closeModal();
    this.fillTree();
    this.addEditItemForm.controls['StructureGeneralDetailFlag'].setValue(1);
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
        this.relateditemsService.DeleteHierarchy(code).subscribe((response) => {
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
    this.relateditemsService.Update(body).subscribe((response) => {
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
            response['errors'],
          life: 6000,
        });
      }
      this.displayModal = false;
    });
    this.addEditItemForm.reset();
    this.addEditItemForm.controls['structureGeneralDetailFlag'].setValue(1);
  }

  // Datatable Methods
  reload() {
    this.rerender();
  }
  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('lookUpsDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, 'RelatedItemSheet.xlsx');
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('General.SuccessExportExcel'),
      detail: this.translateService.instant('General.SuccessExportExcel'),
      life: 6000,
    });
  }
  Print() {
    let element = document.getElementById('lookUpsDataTable');
    let newWin = window.open('');
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('lookUpsDataTable');
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
  ResetTable() {
    this.structureCode = '';
    this.reload();
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
