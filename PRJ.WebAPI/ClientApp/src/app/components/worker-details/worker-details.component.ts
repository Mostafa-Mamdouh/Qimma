import { WorkerService } from 'src/app/services/Worker/worker.service';
import {
  ChangeDetectorRef,
  Component,
  forwardRef,
  Inject,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import * as moment from 'moment';

import { FormArray, FormBuilder, Validators } from '@angular/forms';
import {
  faListDots,
  faPen,
  faGlobe,
  faPlus,
  faXmark,
  faCircleUser,
  faHouse,
  faBrain,
  faUsersLine,
  faPersonBurst,
  faEnvelope,
} from '@fortawesome/free-solid-svg-icons';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { DataTableDirective } from 'angular-datatables';
import { DTOWorkers, Worker } from 'src/app/models/Worker/worker';
import { InsertNewRecordService } from 'src/app/services/Insert-New-Recored/insert-new-record.service';

// import { DataTableDirective } from 'angular-datatables';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexYAxis,
  ApexXAxis,
} from 'ng-apexcharts';
import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';
import { AppComponent } from 'src/app/app.component';
import { CommonService } from 'src/app/services/common/common.service';
import { FacilityProfileService } from 'src/app/services/FacilityProfile/facility-profile.service';
import { EntityProfileService } from 'src/app/services/EntityProfile/entity-profile.service';
import { LicenseProfileService } from 'src/app/services/LicenseProfile/license-profile.service';
import { PractiseProfileService } from 'src/app/services/PractiseProfile/practise-profile.service';
import {Location} from '@angular/common';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
};
@Component({
  selector: 'app-worker-details',
  templateUrl: './worker-details.component.html',
  styleUrls: ['./worker-details.component.css'],
})
export class WorkerDetailsComponent implements OnInit {
  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,

    private sharedService: SharedService,
    private workerService: WorkerService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private commonservice : CommonService,
    private translateService: TranslateService,
    private insertNewRecordService: InsertNewRecordService,
    private Facilityservice: FacilityProfileService,
    private Entityservice: EntityProfileService,
    private Licenseervice: LicenseProfileService,
    private PractiseProfile: PractiseProfileService,
    private _location: Location
  ) {}
  @ViewChild('chart') chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  faPlus = faPlus;
  faXmark = faXmark;
  faCircleUser = faCircleUser;
  faHouse = faHouse;
  faBrain = faBrain;
  faPersonBurst = faPersonBurst;
  faUsersLine = faUsersLine;
  faEnvelope = faEnvelope;
  faGlobe = faGlobe;
  faPen = faPen;
  faListDots = faListDots;
  lang: string;
  langServiceSupscribtion: Subscription;
  dtOptions: any = {};
  YearsList:any;
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  WorkerDetails: any;
  IDWorker: any;
  Update: boolean = true;
  StatusList: any;
  radionuclides: any = [];
  DosimeterType : any ;
  HistoryDos:any;
  StatusWorker : string;
  LastStatusWorker : string;
  GenderList:any;
  EditStatus : boolean = false;
  QuarterList:any;
  WorkerJobPositionsList:any;
  LastReading = {
    quarterCode : '0' , 
    shallowDose : '0' ,
    deepDose: 0
  };
  objData: DTOWorkers = <DTOWorkers>{
    'id' : '',
    'Facility': '',
    'License' : '',
    'Entity': '',
    'Practise' : '',
  };
  WorkerForm = this.fb.group({
    workerNameAr: ['', Validators.required],
    workerNameEn: ['', Validators.required],
    gender : ['', Validators.required],
    passportNo: ['', Validators.required],
    nationalityId: ['', Validators.required],
    nationality: ['', Validators.required],
    jobPosition: ['', Validators.required],
    facilityNameEn: ['', Validators.required],
    countryNameEn: ['', Validators.required],
    mobileNo: ['', Validators.required],
    facilityId : ['',],
    birthDate: [],
    status :[''],
    currentLicense : [''],
    currentPractise : ['']
  })
  Facility:any;
  entities: any[] = [];
  facilities: any[] = [];
  licenses?: any[] = [];
  EntityList : any;
  LicenseList:any;
  dataList: any[] = [];
  PractiseList : any ;
  lookupsList?: any = [];
  Facilities?: any = [];
  currentEntity?: any;
  currentFacility?: any;
  currentLicense?: any;
  math = Math;
  WorkerInfo : any;
  addWorker(Worker: Worker) {
    console.log('Worker');
    console.log(Worker);
    this.WorkerInfo = Worker;
    this.WorkerForm.patchValue({
      workerNameAr : Worker.workerNameAr,
      workerNameEn : Worker.workerNameEn,
      passportNo :Worker.passportNo,
      nationalityId : Worker.nationalityId,
      nationality : Worker.nationality,
      jobPosition : Worker.jobPosition,
      facilityNameEn :Worker.facilityProfile.facilityNameEn,
      facilityId : Worker.facilityId,
      countryNameEn : Worker.basCountries.countryNameEn,
      status : Worker.lookupSetTerm.displayNameEn,
      gender : Worker.gender,
      mobileNo : Worker.mobileNo,
      currentLicense : Worker.currentLicense,
      currentPractise : Worker.currentPractise,
      birthDate : moment(Worker.birthDate).format("yyyy-MM-DD")
    })
   //this.StatusWorker = Worker.lookupSetTerm.id;
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
      console.log(res)
      this.PractiseList = res['data'];
      this.objData.Practise = res['data'][0];
    })
  }
  getQuarter(){
    this.workerService.getQuarter().subscribe((res) => {
      this.QuarterList = res['data'];
    })
  }
  DoseForm = this.fb.group({
    id: [],
    quarterCode: ['', Validators.required],
    dosimeterType: ['', Validators.required],
    dosimeterID: ['', Validators.required],

    deepDose: ['', Validators.required],
    deepDoseUOM : [1],
    shallowDoseUOM : [1],
    shallowDose: ['', Validators.required],
    FiscalYear : ['', Validators.required],
  });

  AssignDosimeter = this.fb.group({
    id: [],
    startWearDate: ['', Validators.required],
    endWearDate: ['', Validators.required],
    dosimeterType: ['', Validators.required],
    dosimeterID: ['', Validators.required], 
    activeFlag : [true]
  })
 
  TransferWorkerForm = this.fb.group({
    id: [],
    TransferDate: ['', Validators.required],
    Status: ['', Validators.required],
    LastStatus: ['', Validators.required],
    Remarks: ['']
  })
  SaveResignForm = this.fb.group({
    id: [],
    Date: ['', Validators.required],
    ResignationReasons: ['', Validators.required],
    Remarks: ['']
  })
  FId : string;
  EId : string; 
  LId : string; 
  License:any;
  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
      // this.updateOptions();
    });
    if(this.route.snapshot.queryParamMap.get('id') != null){
      this.IDWorker = this.route.snapshot.queryParamMap.get('id');
      this.GetWorkerDetails();
    }
    this.route.queryParams.subscribe((params) => {
      params['id'];
      this.FId = params['F']
      this.EId = params['E']
      this.LId = params['L']
      this.objData.Entity = this.EId;
     });
     this.getHeadeDataById();
     this.GetLookupWorkerJobPositions();
     this.getYears();
     this.License = this.LId;
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
    this.GetLastReading();
    this.getQuarter();
    this.GetLookupGender();
    this.getStatus();
    this.getDosimeterTypeFromLookup();
  }

  getDosimeterTypeFromLookup(){
    this.workerService.getLookupByClassName('DosimeterType').subscribe((res) => {
      console.log(res);
      this.DosimeterType = res['data']['lookupSetTerms'];
    });
  }
  
 
  GetWorkerDetails() {
    this.workerService.getById(this.IDWorker).subscribe((res) => {
      this.WorkerDetails = res['data'];
      this.StatusWorker = res['data'].status;
      this.LastStatusWorker = res['data'].status;
      this.TransferWorkerForm.patchValue({
        LastStatus : res['data'].status
      })
      console.log(res['data']);
      this.addWorker(res['data']);
    });
  }

  back() {
    //this.router.navigateByUrl('worker');
    this._location.back();
  }

  getStatus() {
    this.workerService
      .getLookupByClassName('CurrentStatus')
      .subscribe((res) => {
        this.StatusList = res['data']['lookupSetTerms'];
        console.log(this.StatusList);
      });
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


  update() {
    this.Update = true;
  }
  UpdateStatus(){
    this.EditStatus = true;
  }
  saveupdated() {
    //this.Update = true;
    this.WorkerForm.markAllAsTouched();
    if (this.WorkerForm.valid) {
      var obj = { ...this.WorkerForm.value, Id: this.IDWorker };
      this.workerService.update(obj).subscribe((res) => {
        if (res) {
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('Saved As Draft'),
            detail: this.translateService.instant('Saved As Draft'),
            life: 6000,
          });

          console.log(res);
        }
      });
    }
  }

    GethistoryDosByUserId(){
      this.workerService.GethistoryDosByUserId(this.IDWorker).subscribe((res) => 
      {
        console.log(res);
         this.HistoryDos = res['data'];
      })
    }
    getYears(){
      this.workerService.getYears().subscribe((res) => {
        this.YearsList = res['data'];
      })
    }
    GetLastReading(){
      this.workerService.GetLastReading(this.IDWorker)
      .subscribe((res) => {
        //if(res['data'].deepDoseres > 0 ){
          console.log('1111')
          console.log(res['data'])
          this.LastReading.deepDose = res['data'].deepDose;
          this.LastReading.quarterCode= res['data'].quarterCode;
          this.LastReading.shallowDose = res['data'].shallowDose;
      //  }
        
      })
    }
    SaveAddDose(){
      this.DoseForm.value.id = this.IDWorker;
      console.log(this.DoseForm.value);
      this.workerService.AddReading(this.DoseForm.value)
      .subscribe((res) => 
      {
        if (res['succeeded']) {
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('Save Successfully'),
            detail: this.translateService.instant('Save Successfully'),
            life: 6000,
          });
          this.GetLastReading();
      }});
    }

    SaveAddDosimeters(){
      console.log(this.AssignDosimeter.value);  
      this.AssignDosimeter.patchValue({
        id: this.IDWorker,
        activeFlag : true
      })
      this.workerService.AddDosimeters(this.AssignDosimeter.value).subscribe((res) => 
      {
        if (res['succeeded']) {
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('Save Successfully'),
            detail: this.translateService.instant('Save Successfully'),
            life: 6000,
          });
      }});
      this.AssignDosimeter.reset();
    }
    
    SaveTransferWorker(){
      this.TransferWorkerForm.value.id = this.IDWorker;
      console.log(this.TransferWorkerForm.value);  
      this.workerService.updatestatus(this.TransferWorkerForm.value).subscribe((res) => {
       this.WorkerForm.patchValue({
        status : this.TransferWorkerForm.value.Status
       })
       
      })
    }
    SaveResign(){
      this.SaveResignForm.value.id = this.IDWorker;
      console.log(this.SaveResignForm.value);  

    }
  }


