import { LicenseProfile } from './../../models/LicenseProfile/LicenseProfile';

import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import UserData from '../../models/Common/userdata';
import { Response } from '../../models/Common/response';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { CommonClass } from 'src/app/common/commonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from "@angular/platform-browser";
import { DataTableDirective } from 'angular-datatables';
import { ClipboardService } from 'ngx-clipboard';
import { DTOWorkers } from 'src/app/models/Worker/worker';
import { AddWorkerService } from 'src/app/services/Worker/Add-worker.service';
import { FacilityProfileService } from 'src/app/services/FacilityProfile/facility-profile.service';
import { CommonService } from 'src/app/services/common/common.service';
import { DatePipe, formatDate , Location} from '@angular/common';
import * as moment from 'moment';
import * as XLSX from 'xlsx';
import { EntityProfileService } from 'src/app/services/EntityProfile/entity-profile.service';
import { LicenseProfileService } from 'src/app/services/LicenseProfile/license-profile.service';
import { PractiseProfileService } from 'src/app/services/PractiseProfile/practise-profile.service';
import { InsertNewRecordService } from 'src/app/services/Insert-New-Recored/insert-new-record.service';
import { WorkerService } from 'src/app/services/Worker/worker.service';

declare var $: any;

@Component({
  selector: 'app-add-new-worker',
  templateUrl: './add-new-worker.component.html',
  styleUrls: ['./add-new-worker.component.css']
})
export class AddNewWorkerComponent implements OnInit,AfterViewInit {

  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { IsPageLoad: true, filter: '' };
  currentUser: UserData = <UserData>{};
  objData: DTOWorkers = <DTOWorkers>{
    'id' : '',
    'WorkerNameAr': '',
    'WorkerNameEn': '',
    'Gender' : '',
    'BirthDate': null,
    'JobPosition': '',
    'Facility': '',
    'License' : '',
    'Entity': '',
    'Practise' : '',
    'NationalityId': '',
    'Status': '',
    'PassportNo': '',
    'Nationality': '',
    'FacilityId' : ''
  };
  HeaderObj = {
    'Facility': '',
    'License' : '',
    'Entity': '',
    'Practise' : '',
  }
  EntityList : any;
  LicenseList:any;
  dataList: any[] = [];
  PractiseList : any ;
  lookupsList?: any = [];
  Facilities?: any = [];
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  GenderList: any;
  WorkerJobPositionsList:any;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  datafacilityList: any = [];
  nationalitys: any;
  currentEntity?: any;
  currentFacility?: any;
  currentLicense?: any;
  FId : string;
  EId : string; 
  LId : string; 
  SubmitForm = new FormGroup({
    WorkerNameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(1)]),
    WorkerNameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(1)]),
    Gender: new FormControl('', [Validators.required]),
    BirthDate: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    JobPosition: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    NationalityId: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(/^[1-9]\d*$/)]),
    Status: new FormControl('',[ Validators.required]),
    PassportNo: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern(/^[1-9]\d*$/)]),
    Entity: new FormControl('', [Validators.required]),
    Facility: new FormControl('', [Validators.required]),
    License: new FormControl(''),
    Practise : new FormControl(''),
    Nationality: new FormControl('', [Validators.required]),
    mobileNo :new FormControl('', [Validators.required]),
    currentLicense : new FormControl(''),
    currentPractise :new FormControl(''),
  })

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private workersservice: AddWorkerService,
    private Facilityservice: FacilityProfileService,
    private Entityservice: EntityProfileService,
    private Licenseervice: LicenseProfileService,
    private PractiseProfile: PractiseProfileService,
    private commonservice: CommonService,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private sharedService: SharedService,
    private ConfirmationService: ConfirmationService,
    private MessageService: MessageService,
    private commonClass: CommonClass,
    private titleService: Title,
    private clipboardApi: ClipboardService ,
    private insertNewRecordService: InsertNewRecordService,
    private workerService :WorkerService,
    private _location : Location
    ) {
    this.route.queryParams.subscribe(params => {
      
    });
  }

  ngOnInit(): void {
    this.getUserData();
    this.getAllWorker();
    this.GetLookupGender();
    this.GetLookupWorkerJobPositions();
    this.route.queryParams.subscribe((params) => {
      params['id'];
      this.FId = params['F']
      this.EId = params['E']
      this.LId = params['L']
      this.objData.Entity = this.EId;
     });
     this.getHeadeDataById();
     this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  getHeadeDataById(){
    this.insertNewRecordService.getEntities().subscribe((res) => {
          this.EntityList = res['data'];
          this.insertNewRecordService.getFacilities(this.EId).subscribe((res) => {
          this.Facilities = res['data'];
          this.insertNewRecordService.getLicenses(this.FId).subscribe((res) => {
          this.LicenseList = res['data'];

          this.workerService.getEntityById(this.EId).subscribe((res) => {
          this.objData.Entity = res['data'];
           })
          this.workerService.getFacilityById(this.FId).subscribe((res) => {
             this.objData.Facility = res['data'];
           })
          this.workerService.getLicenseById(this.LId).subscribe((res) => {
              this.objData.License = res['data'];
              console.log('license by id')
              console.log(res['data'])
              console.log(this.LicenseList )
              this.getPractiseByLicenseId(this.LId);  
           });
          })
        });
    });
  }
  dtInstanceObj: any;
  initialized: boolean = false;
  date = new Date;
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
   // _this.currentUser=_this.userService.getCurrentUser();
    _this.getStatus();
    _this.getEntity();
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
        dataTablesParameters.extra = this.LId;
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

  GetLookupGender(){
    this.commonservice.getLookupByClassName('Gender').subscribe((res) => {
      this.GenderList = res['data']['lookupSetTerms'];
      console.log(this.GenderList)
    })
  }
  GetLookupWorkerJobPositions(){
    this.commonservice.getLookupByClassName('WorkerJobPositions').subscribe((res) => {
      this.WorkerJobPositionsList = res['data']['lookupSetTerms'];
      console.log(this.WorkerJobPositionsList)
    })
  }
  

  // getFacilitiesByEntityId(id: string) {
  //   this.insertNewRecordService.getFacilities(id).subscribe((res) => {
  //     this.facilities = res['data'];
  //     this.sharedService.currentEntity.next(this.currentEntity);
  //     if (this.sharedService.currentFacility.value) {
  //       this.currentFacility = this.sharedService.currentFacility.value;
  //       this.getLicensesByFacilityId(this.currentFacility.id);
  //     }
  //   });
  // }
  
  // Get Licenses by Facility
  // getLicensesByFacilityId(id: string) {
  //   this.insertNewRecordService.getLicenses(id).subscribe((res) => {
  //     this.licenses = res['data'];
  //     if (this.sharedService.currentLicense.value) {
  //       this.currentLicense = this.sharedService.currentLicense.value;
  //       this.setLicense();
  //     }
  //   });
  // }

  //////////////////////////
setFacility() {
  this.sharedService.currentFacility.next(this.currentFacility);
  this.getLicensesByFacilityId(this.currentFacility.id);
}

getFacilitiesByEntityId(id: string) {
  this.insertNewRecordService.getFacilities(id).subscribe((res) => {
    this.Facilities = res['data'];
    this.sharedService.currentEntity.next(this.currentEntity);
    if (this.sharedService.currentFacility.value) {
      this.currentFacility = this.sharedService.currentFacility.value;
      this.getLicensesByFacilityId(this.currentFacility.id);
    }
  });
}

// Get Licenses by Facility
getLicensesByFacilityId(id: string) {
  this.insertNewRecordService.getLicenses(id).subscribe((res) => {
    this.LicenseList = res['data'];
    if (this.sharedService.currentLicense.value) {
      this.currentLicense = this.sharedService.currentLicense.value;
      this.setLicense();
    }
  });
}
setLicense() {
  this.sharedService.currentLicense.next(this.currentLicense);
}
  reload() {
    var _this = this;
    _this.getData();
    _this.rerender();
  }
  reset() {
    this.Update = 0;
   // this.SubmitForm.reset();
    this.SubmitForm.patchValue({
      WorkerNameAr: '',
      WorkerNameEn:'' ,
      Gender: '',
      BirthDate:'' ,
      JobPosition:'',
      NationalityId: '',
      Status: '',
      PassportNo: '',
      Nationality: '',
      mobileNo :'', 
    });
  }
  Details(obj: any) {
    //this.objData = <DTOWorkers>{};
    console.log(obj);
    this.objData.BirthDate = moment(obj.birthDate).format("yyyy-MM-DD");
    this.objData.id = obj.id;
    this.objData.WorkerNameAr = obj.workerNameAr;
    this.objData.WorkerNameEn = obj.workerNameEn;
    console.log(obj.jobPosition);
    this.objData.JobPosition = obj.jobPosition;
    this.objData.NationalityId = obj.nationalityId;
    //this.objData.Facility = obj.facilityProfile.id;
    this.objData.Status = obj.status;
    this.objData.Gender = obj.gender;
    this.objData.Nationality = obj.nationality;
    this.objData.PassportNo = obj.passportNo;
   // this.objData.License = obj.license;
  //  this.objData.Practise = obj.practise;
    this.objData.mobileNo = obj.mobileNo;
    this.objData.FacilityId= obj.facilityProfile.id;
    this.Update = 1;

  }
  save() {
    var _this = this;
    console.log('ss');
    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      this.SubmitForm.markAllAsTouched();
      if (_this.SubmitForm.valid) {
        var objSave = {
          "WorkerNameAr": _this.SubmitForm.controls['WorkerNameAr'].value,
          "WorkerNameEn": _this.SubmitForm.controls["WorkerNameEn"].value,
          "JobPosition": _this.SubmitForm.controls["JobPosition"].value,
          "BirthDate": _this.SubmitForm.controls["BirthDate"].value,
          "NationalityId": _this.SubmitForm.controls["NationalityId"].value,
          "FacilityId": _this.SubmitForm.controls["Facility"].value['id'],
          "Status": _this.SubmitForm.controls["Status"].value,
          "PassportNo": _this.SubmitForm.controls["PassportNo"].value,
          "Nationality": _this.SubmitForm.controls["Nationality"].value,
          "Gender": _this.SubmitForm.controls["Gender"].value,
          "CurrentLicense" : _this.SubmitForm.controls["License"].value['id'],
          "CurrentPractise" : _this.SubmitForm.controls["Practise"].value['id'],
          "mobileNo" : _this.SubmitForm.controls['mobileNo'].value
        };
        console.table(objSave);
        console.log(objSave);

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
      console.log(this.SubmitForm.value)
      this.SubmitForm.markAllAsTouched();
      if (_this.SubmitForm.valid) {
        var objUpdate = {
          "Id": _this.objData.id,
          "WorkerNameAr": _this.objData.WorkerNameAr,
          "WorkerNameEn": _this.objData.WorkerNameEn,
          "JobPosition": _this.objData.JobPosition,
          "BirthDate": _this.objData.BirthDate,
          "NationalityId": _this.objData.NationalityId,
          "facilityId": _this.objData.FacilityId,
          "Status": _this.objData.Status,
          "PassportNo": _this.objData.PassportNo,
          "Nationality": _this.objData.Nationality,
          "Gender": _this.objData.Gender,
          "CurrentLicense" : _this.LId,
          "CurrentPractise" :_this.HeaderObj.Practise,
          "mobileNo" : _this.objData.mobileNo
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
  getEntity() {
    this.Entityservice.getAll().subscribe((res) => {
      this.EntityList = res['data'];
      console.log(this.EntityList)
    })
  }
  getFacilityProfileByEntityId(Id :any ) {
    this.Facilityservice.getFacilityProfileByEntityId(Id).subscribe((res) => {
      this.Facilities = res['data'];
      console.log(this.Facilities)
    })
  }
  getLicenseByFacilityId(Id : any){
    this.Licenseervice.getLicenseProfileByFacilityId(Id).subscribe((res) => {
      this.LicenseList = res['data'];
      console.log(this.LicenseList)
    })
  }
  getPractiseByLicenseId(Id : any){
    this.PractiseProfile.getPractiseProfileByLicenseId(Id).subscribe((res) => {
      this.PractiseList = res['data'];
      this.objData.Practise = res['data'][0];
      this.HeaderObj.Entity = this.EId;
      this.HeaderObj.Facility = this.FId;
      this.HeaderObj.License = this.LId;
      this.HeaderObj.Practise = res['data'][0].id;
      console.log(this.PractiseList)
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
    this._location.back();

    //this.router.navigate(['/worker']);
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
