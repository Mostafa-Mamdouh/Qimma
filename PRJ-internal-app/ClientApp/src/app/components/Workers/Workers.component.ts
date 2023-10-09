import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { UserInfoServices } from '../../../services/userinfo.service';
import UserData from '../../../models/Common/userdata';
import { Response } from '../../../models/Common/response';
import BasCountries from '../../../models/Common/BasCoutries';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from "@angular/platform-browser";
import { LookupSet } from '../../../models/LookupSet/LookupSet';
import { DataTableDirective } from 'angular-datatables';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { DTOWorkers } from '../../../models/Workers/Workers';
import { WorkersServices } from '../../../services/Workers/Workers.service';
import { FacilityServices } from '../../../services/Facility/facility.service';
import { CommonServices } from '../../../services/Common/common.service';
import { DatePipe, formatDate } from '@angular/common';
import * as moment from 'moment';
declare var $: any;

@Component({
  selector: 'Workers',
  templateUrl: './Workers.component.html',
  styleUrls: ['./Workers.component.css']
})

export class WorkersComponent implements OnInit, AfterViewInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { IsPageLoad: true, filter: '' };
  currentUser: UserData = <UserData>{};
  objData: DTOWorkers = <DTOWorkers>{};
  dataList: any[] = [];
  lookupsList?: any = [];
  Facilities?: any = [];
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  datafacilityList: any = [];
  nationalitys: any;
  SubmitForm = new FormGroup({
    WorkerNameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(1)]),
    WorkerNameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(1)]),
    BirthDate: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    JobPosition: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    NationalityId: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(/^[1-9]\d*$/)]),
    Status: new FormControl( '',[ Validators.required]),
    PassportNo: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(/^[1-9]\d*$/)]),
    Facility: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    Nationality: new FormControl('', [Validators.required])
  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private workersservice: WorkersServices,
    private Facilityservice: FacilityServices,
    private userService: UserInfoServices,
    private commonservice: CommonServices,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private sharedService: SharedService,
    private ConfirmationService: ConfirmationService,
    private MessageService: MessageService,
    private commonClass: CommonClass,
    private titleService: Title,
    private clipboardApi: ClipboardService  ) {
    this.route.queryParams.subscribe(params => {
      
    });
  }

  ngOnInit(): void {
    this.getUserData();
    this.getAllWorker();
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  dtInstanceObj: any;
  initialized: boolean = false;

  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('Workers.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
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
  getUserData(): void {
    var _this = this;
    _this.currentUser=_this.userService.getCurrentUser();
    _this.getStatus();
    _this.getFacility();
    _this.getNationality();
    _this.getData();
  }
  getData() {
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
        this.workersservice
          .GetAllFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log(resp);
              this.dtTrigger.next();
              this.dataList = resp['data'];
              console.log(this.dataList);
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
        { data: 'workerNameAr' },
        { data: 'workerNameEn' },
        { data: 'jobPosition' },
        { data: 'facilityProfile' },
        { title:'actions', data: null }
      ],
    };
  }
  getAllWorker() {
    this.workersservice.GetAll().subscribe((res) => {
      console.log(res)
    })
  }
  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.workerNameEn,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {

        _this.workersservice.Delete(obj.id).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.DeletedSuccessfully'), detail: _this.translateService.instant('General.DeletedSuccessfully'), life: 6000 });
          }
          else if (response.succeeded == false && response.message == "NOTEXIST") {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.NotFound'), detail: this.translateService.instant('General.NotFound'), life: 6000 });
          } else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
          }
        })
      },
      reject: () => {
        _this.app.messageService.add({ severity: 'warn', key: 'PlanValidation', summary: _this.translateService.instant('General.DeleteCancelled'), detail: _this.translateService.instant('General.DeleteCancelled'), life: 6000 });
      }
    })

  }
  reload() {
    var _this = this;
    _this.getData();
    _this.rerender();
  }
  reset() {
    this.Update = 0;
    this.SubmitForm.reset();
  }
  Details(obj: any) {
    this.objData = <DTOWorkers>{};
    var x = moment(obj.birthDate).toDate();
    this.objData.BirthDate = moment(obj.birthDate).format("yyyy-MM-DD");
    this.objData.id = obj.id;
    this.objData.WorkerNameAr = obj.workerNameAr;
    this.objData.WorkerNameEn = obj.workerNameEn;
    this.objData.JobPosition = obj.jobPosition;
    this.objData.NationalityId = obj.nationalityId;
    this.objData.Facility = obj.facilityProfile.id;
    this.objData.Status = obj.status;
    this.objData.Nationality = obj.nationality;
    this.objData.PassportNo = obj.passportNo;
    this.Update = 1;
  }
  save() {
    var _this = this;
    var lang = _this.translateService.currentLang;
    alert(this.SubmitForm.markAllAsTouched())
    alert(_this.SubmitForm.valid)

    if (_this.Update == 0) {
      this.SubmitForm.markAllAsTouched();
      if (_this.SubmitForm.valid) {
        var objSave = {
          "WorkerNameAr": _this.SubmitForm.controls['WorkerNameAr'].value,
          "WorkerNameEn": _this.SubmitForm.controls["WorkerNameEn"].value,
          "JobPosition": _this.SubmitForm.controls["JobPosition"].value,
          "BirthDate": _this.SubmitForm.controls["BirthDate"].value,
          "NationalityId": _this.SubmitForm.controls["NationalityId"].value,
          "FacilityId": _this.SubmitForm.controls["Facility"].value,
          "Status": _this.SubmitForm.controls["Status"].value,
          "PassportNo": _this.SubmitForm.controls["PassportNo"].value,
          "Nationality": _this.SubmitForm.controls["Nationality"].value
        };
        _this.workersservice.Add(objSave).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.SubmitForm.reset();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
          } else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
          }
        })
      }
    }
    else {
      this.SubmitForm.markAllAsTouched();
      if (_this.SubmitForm.valid) {
        var objUpdate = {
          "Id": _this.objData.id,
          "WorkerNameAr": _this.objData.WorkerNameAr,
          "WorkerNameEn": _this.objData.WorkerNameEn,
          "JobPosition": _this.objData.JobPosition,
          "BirthDate": _this.objData.BirthDate,
          "NationalityId": _this.objData.NationalityId,
          "FacilityId": _this.objData.Facility,
          "Status": _this.objData.Status,
          "PassportNo": _this.objData.PassportNo,
          "Nationality": _this.objData.Nationality
        };
        var _this = this;
        _this.workersservice.Update(objUpdate).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.SubmitForm.reset();
            _this.Update = 0;
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
          } else if (response.succeeded == false && response.message == "NOTEXIST") {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.NotFound'), detail: this.translateService.instant('General.NotFound'), life: 6000 });
          } else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
          }
        })
      }
     
    }
  }

  getStatus() {
    this.commonservice.getLookupByClassName('CurrentStatus').subscribe((res) => {
      this.lookupsList = res['data']['lookupSetTerms'];
      console.log(this.lookupsList)
    })
  }
  getFacility() {
    this.Facilityservice.GetAll().subscribe((res) => {
      this.Facilities = res['data'];
      console.log(this.Facilities)
    })
  }
  getNationality() {
    this.workersservice.GetAllNationality().subscribe((res) => {
      this.nationalitys = res['data'];
    })
  }
  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('WorkersDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "WorkersSheet.xlsx");
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessExportExcel'), detail: this.translateService.instant('General.SuccessExportExcel'), life: 6000 });
  }
  Print() {
    let element = document.getElementById('WorkersDataTable');
    let newWin = window.open("");
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('WorkersDataTable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessCopy'), detail: this.translateService.instant('General.SuccessCopy'), life: 6000 });
  }
  Back() {
    this.router.navigate(['system-settings']);
  }
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }
  isNumberKey(evt: any) {
    return this.commonClass.isNumberKey(evt);
  }
  OnlyArabic(event: any, lng: any) {
    return this.commonClass.OnlyArabic(event, lng);
  }
  OnlyEnglish(event: any, lng: any) {
    return this.commonClass.OnlyEnglish(event, lng);
  }

  LookUpTerm(obj: any) { 
    this.router.navigate(['lookup-set-term'], { queryParams: { Id: obj.id } });
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
