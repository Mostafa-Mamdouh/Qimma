import { Component, forwardRef, Inject, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api'
import { TranslateService } from '@ngx-translate/core';
import { RadionuclideService } from '../../../services/Radionuclide/radionuclide.service';
import { AppComponent } from '../../app.component';
import { HttpClient } from '@angular/common/http';
import { faTrash, faPenToSquare, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { Route, Router } from '@angular/router';
import { CommonServices } from '../../../services/Common/common.service';
import { RadionuclideModel } from '../../../models/Radionuclide/radionuclideModel';
import { Response } from '../../../models/Common/response';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { error } from 'jquery';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { ActivityUnits } from '../../../Enumerations/ActivityUnits';
import { TimeUnits } from '../../../Enumerations/Enums';

@Component({
  selector: 'app-radionucliede-setup',
  templateUrl: './radionucliede-setup.component.html',
  styleUrls: ['./radionucliede-setup.component.css']
})
export class RadionucliedeSetupComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  lang: string = 'en';

  radionuclides: any = [];
  activityUnits?: any = [];
  timeUnits?: any = [];
  DataList: any;

  ActivityUnits = ActivityUnits;
  TimeUnits = TimeUnits;

  rtl: boolean = true;
  actionType: string = 'add'

  inEditModeId;

  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private fb: FormBuilder,
    private commonServices: CommonServices,
    private confirmationService: ConfirmationService,
    private translateService: TranslateService,
    private radionuclideService: RadionuclideService,
    private http: HttpClient,
    private router: Router,
    private clipboardApi: ClipboardService  ) {

  }

  addNewRadionuclideForm = this.fb.group({
    isotop: ['', [Validators.required, Validators.maxLength(200)]],
    displayName: ['', [Validators.required, Validators.maxLength(75)]],
    dValue: [null, Validators.required],
    dValueUnit: ['', [Validators.required, Validators.maxLength(200)]],
    halfLife: [null, Validators.required],
    halfLifeUnit: ['', [Validators.required, Validators.maxLength(200)]],
    // initialActivity: [10],
    // activityUnit: [],
    exemptionValue: [null, Validators.required],
      exemptionValueUnit: ['', [Validators.required,Validators.maxLength(200)]],
    isActive: [false, Validators.required],
  })

  editEvent(load) {
    this.inEditModeId = load.radionuclideId
    this.addNewRadionuclideForm.patchValue(load);
    this.actionType = 'update'
  }

  deleteEvent(row) {
    
    this.confirmationService.confirm({
      message: this.translateService.instant('Confirmation.deleteMsg') + " " + this.translateService.instant('Radionuclide.radionuclide') + ": " + row.isotop,
      header: this.translateService.instant('Confirmation.confirm'),
      acceptLabel: this.translateService.instant('Confirmation.Yes'),
      rejectLabel: this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.radionuclideService.Delete(row.id).subscribe(res => {
          
          this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: "Deleted", detail: 'Deleted successfuly', life: 6000 });
          this.reload();
        })
      },
      reject: () => {
        this.app.messageService.add({ severity: 'warn', key: 'PlanValidation', summary: "Deleted", detail: 'Delete canceled', life: 6000 });
      }
    })
  }

  getRadionulcides() {
    var _thss = "";
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('LookUps.PageTitle').subscribe(valueTitle => {
        _thss = value + valueTitle;
      });
    });

    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: true,
      serverSide: true,
      searching: true,
      ajax: (dataTablesParameters: any, callback, settings) => {
        dataTablesParameters.searchCriteria = this._searchCriteria;
        console.log(dataTablesParameters)
        this.radionuclideService
          .GetRadionulicdesFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log(resp)
              this.dtTrigger.next();
              this.radionuclides = resp['data'];
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: resp['data'],
              });
            } else {
              this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.Error'), detail: this.translateService.instant('General.Error') + " " + resp['errors'].join('=>'), life: 6000 });
            }
          });
      },
      columns: [
        { data: 'isotop' },
        { data: 'displayName' },
        { data: 'halfLife' },
        //{ data: 'initialActivity' },
        { data: 'exemptionValue' },
        { data: 'isActive' }, 
        { data: null }, 
      ],
    };
  }

  ngOnInit(): void {

    this.getRadionulcides();


    this.commonServices.getLookupByClassName('ActivityUnit').subscribe((res) => {
      this.activityUnits = res['data']['lookupSetTerms'];
    })

    this.commonServices.getLookupByClassName('TimeUnits').subscribe((res) => {
      this.timeUnits = res['data']['lookupSetTerms'];
    })
  }

  onSave() {
    var _this = this;
    let load = this.addNewRadionuclideForm.value;
    console.log(load)
    if (this.addNewRadionuclideForm.valid) {

      this.radionuclideService.Add(load).subscribe(data => {
        var response = data as Response<boolean>;
        if (response.succeeded && response.data) {
          this.addNewRadionuclideForm.reset();
          this.addNewRadionuclideForm.controls['isActive'].setValue(false);
          this.reload();
          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else if (response.succeeded == false && response.message == "EXISTS") {
          this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.Exist'), detail: this.translateService.instant('General.Exist'), life: 6000 });
        }
        else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      });

    }
    else {
      this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.RequiredAllFields'), detail: this.translateService.instant('General.RequiredAllFields'), life: 6000 });
    }
  }

  onUpdate() {
    var _this = this;

    let load = _this.addNewRadionuclideForm.value;
    let body = { ...load, radionuclideId: this.inEditModeId }

    _this.radionuclideService.Update(body).subscribe(function (data) {
      var response = data as Response<boolean>;
      if (response.succeeded && response.data) {
        _this.radionuclides = [];
       
        _this.reset();
        _this.addNewRadionuclideForm.controls['isActive'].setValue(false);
        _this.reload();
        _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });

      } else if (response.succeeded == false && response.message == "NOTEXIST") {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.NotFound'), detail: _this.translateService.instant('General.NotFound'), life: 6000 });
      } else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }

  reload() {
    var _this = this;
    _this.getRadionulcides();
    _this.rerender();
  }
  reset() {
    this.inEditModeId = null;
    this.actionType = 'add'
    this.addNewRadionuclideForm.reset();
  }

  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('raidonuclidesDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "RaidonuclidesSheet.xlsx");
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessExportExcel'), detail: this.translateService.instant('General.SuccessExportExcel'), life: 6000 });

  }
  Print() {
    let element = document.getElementById('raidonuclidesDataTable');
    let newWin = window.open("");
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('raidonuclidesDataTable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessCopy'), detail: this.translateService.instant('General.SuccessCopy'), life: 6000 });
  }

  Back() {
    this.router.navigate(['system-settings']);
  }
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
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

  dtInstanceObj: any;
  initialized: boolean = false;
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
}
