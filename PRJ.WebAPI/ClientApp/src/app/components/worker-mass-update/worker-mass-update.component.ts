import { AutoCompleteModule } from 'primeng/autocomplete';
import { WorkerService } from 'src/app/services/Worker/worker.service';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  forwardRef,
  Inject,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import * as FileSaver from 'file-saver';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { faListDots,faPen,faGlobe, faPlus, faXmark ,faCircleUser,faHouse, faBrain,faUsersLine,faPersonBurst,faEnvelope} from '@fortawesome/free-solid-svg-icons';
import { finalize, Subject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { DataTableDirective } from 'angular-datatables';
import { ActivatedRoute, Router } from '@angular/router';
import { AddWorkerService } from 'src/app/services/Worker/Add-worker.service';
import { FacilityProfileService } from 'src/app/services/FacilityProfile/facility-profile.service';
import { EntityProfileService } from 'src/app/services/EntityProfile/entity-profile.service';
import { PractiseProfileService } from 'src/app/services/PractiseProfile/practise-profile.service';
import { LicenseProfileService } from 'src/app/services/LicenseProfile/license-profile.service';
import { CommonService } from 'src/app/services/common/common.service';
import { DTOWorkers,QuarterModel,MassUpdateDataTable } from 'src/app/models/Worker/worker';
import { InsertNewRecordService } from 'src/app/services/Insert-New-Recored/insert-new-record.service';
import { DatePipe, formatDate , Location} from '@angular/common';
import { HttpClient, HttpEventType } from '@angular/common/http';
import * as _ from 'lodash';
import { AppComponent } from 'src/app/app.component';
import { TranslateService } from '@ngx-translate/core';
import { CommonClass } from 'src/app/common/commonClass';
import { Title } from '@angular/platform-browser';
import { noAuto } from '@fortawesome/fontawesome-svg-core';
import { noConflict } from 'jquery';

@Component({
  selector: 'app-worker-mass-update',
  templateUrl: './worker-mass-update.component.html',
  styleUrls: ['./worker-mass-update.component.css']
})
export class WorkerMassUpdateComponent implements OnInit ,OnChanges{
  faListDots =faListDots;
  lang: string;
  langServiceSupscribtion: Subscription;
  dtOptions: DataTables.Settings = {};
  data: any[] = [];

  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  problemsImportActive = false;
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  radionuclides: any = [];
  EntityList : any;
  LicenseList:any;
  dataList: any[] = [];
  PractiseList : any ;
  lookupsList?: any = [];
  Facilities?: any = [];
  fileUploadForm: FormGroup;
  fileInputLabel: string;
  selectedYearObj = {fiscalYear: new Date().getFullYear.toString()};
  selectedQuarterObj : any;
  problemsImport = [];
  selectedYear = this.selectedYearObj.fiscalYear;
  @ViewChild('UploadFileInput', { static: false }) uploadFileInput: ElementRef;
  
  @Input()
    requiredFileType:string;
    ItemsRow: FormGroup;
    fileName = '';
    uploadProgress:number;
    uploadSub: Subscription;
  objData: DTOWorkers = <DTOWorkers>{
    'id' : '',
    'Facility': '',
    'License' : '',
    'Entity': '',
    'Practise' : '',
  };
    SubmitForm = new FormGroup({
    Entity: new FormControl('', [Validators.required]),
    Facility: new FormControl('', [Validators.required]),
    License: new FormControl('', [Validators.required]),
    Practise : new FormControl('', [Validators.required]),
    Nationality: new FormControl('', [Validators.required])
  });
  FId : string;
  EId : string; 
  LId : string; 
 file : any;
  QuarterList:Array<QuarterModel>;
  QuarterNow : number;
  YearsList:any;
  importExcelFile = new FormGroup({
    quarter: new FormControl('',[Validators.required]),
    year:new FormControl('',[Validators.required]),
    formFile : new FormControl([File],[Validators.required])
  })
  ExportExcelFile = new FormGroup({
    year:new FormControl('',[Validators.required]),
    quarter: new FormControl('',[Validators.required]),
  })


  constructor(  
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
      private workerService :WorkerService,
    private router:Router,
    private workersservice: AddWorkerService,
    private Facilityservice: FacilityProfileService,
    private Entityservice: EntityProfileService,
    private Licenseervice: LicenseProfileService,
    private PractiseProfile: PractiseProfileService,
    private commonservice: CommonService,
    private insertNewRecordService: InsertNewRecordService,
    private sharedService: SharedService,
    private route: ActivatedRoute,
    private _location : Location,
    private fb: FormBuilder,
    private http: HttpClient,
    private translateService: TranslateService,
    private commonClass: CommonClass,
    private titleService: Title,


    ) { }
    ngOnChanges(changes: SimpleChanges) {
      // changes.prop contains the old and the new value...
      console.log("Changes detected");
    }
    @ViewChild(DataTableDirective)
    datatableElement: DataTableDirective;
  
  ngOnInit(): void {
    this.getQuarter();
    this.getEntity();
    this.getYears();
    this.route.queryParams.subscribe((params) => {
      params['id'];
      this.FId = params['F']
      this.EId = params['E']
      this.LId = params['L']
      this.objData.Entity = this.EId;
     });
     this.fileUploadForm = this.fb.group({
      myfile: ['']
    })
     this.getHeadeDataById();
     this.handleDate();

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
  back(){
    this._location.back();
  }
  getQuarter(){
    this.workerService.getQuarter().subscribe((res) => {
      console.log(res['data'])
      this.QuarterList = res['data'];
    })
  }
  
  SelectedQuarter(quarterCode){
    this.QuarterNow = quarterCode;
    console.log(this.selectedQuarterObj);
    this.rerender();
  }

 
  isNumberKey(evt: any) {
    return this.commonClass.isNumberKey(evt);
  }

  getYears(){
    this.workerService.getYears().subscribe((res) => {
      this.YearsList = res['data'];
      console.log(this.YearsList)
    })
  }
  SelectedYear(id:any){
  this.selectedYear = id;
  this.rerender();
  }
 
  reload() {
    var _this = this;
    _this.getdata();
    _this.rerender();
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
 
 getQuarterByMonth(month :string){
  this.workerService.GetQuarterByMonth(month).subscribe((res) => {
    this.QuarterNow = res['data'];
    this.selectedQuarterObj = this.QuarterList.find((obj) => {
      return obj.quarterCode == res['data'];
    });

  })
 }

  handleDate(){
    var DateNow = new Date();
    var month = DateNow.getMonth() + 1;
    var year = DateNow.getFullYear();
    if(month < 4){
      year = year - 1 ;
      month = (month + 12) - 3;
    }
    else {
      month = month - 3;
    }
   this.selectedYearObj.fiscalYear = year.toString();
   
   this.getQuarterByMonth(month.toString());
   this.getdata();
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
      console.log(this.PractiseList)
    })
  }
 
  
  
  getdata(){
    this.dtOptions = {
    paging: true,
    pagingType: 'full_numbers',
    pageLength: 10,
    autoWidth: true,
    serverSide: true,
    processing: true,
    searching: true,
    deferRender:true,
    ajax: (dataTablesParameters: any, callback, settings) => {
      console.log('dataTablesParameters:', dataTablesParameters);
      dataTablesParameters.searchCriteria = this._searchCriteria;
      dataTablesParameters.extra = this.LId;
      dataTablesParameters.extra2 = this.selectedYearObj.fiscalYear;
      dataTablesParameters.lang = this.lang == 'en' ? 1 : 2;
      this.workerService
        .GetAllFunctionalMassUpdate(dataTablesParameters)
        .subscribe((resp) => {
          console.log('resp:', resp);
          this.dtTrigger.next();
          this.radionuclides = resp['data'];
          callback({
            recordsTotal: resp['recordsTotal'],
            recordsFiltered: resp['recordsFiltered'],
            data: resp['data'],
          });
        });
    },
    // MassUpdateDataTable
    columns: [
      { data: null },
      { data: this.lang =="en" ? 'workerNameEn' :'workerNameAr' },
      { data:  'nationalityId'},
      { data: this.lang =="en" ? 'genderEn' : 'genderAr' },
      { data:  'birthDate' },
      { data:this.lang =="en" ?  'jobPositionEn' : 'jobPositionAr'},
      { data : this.lang =="en" ? 'statusEn' : 'statusAr'},
      { data: 'q1Value'},
      { data: 'q2Value' },
      { data: 'q3Value' },
      { data: 'q4Value' },
      { data: null },
    ],
  };
}
 
AddReadingWithDataTable(){
  var ReadingData = []
  
  for(var i =0 ; i< this.radionuclides.length ; i++){
    ReadingData.push({
      workerId : this.radionuclides[i].id,
      deepDose : this.radionuclides[i].DeepDose
    }) 
  }
  this.workerService.AddReadingFromDataTable(ReadingData , this.selectedYearObj.fiscalYear , this.QuarterNow.toString()).subscribe((res) => 
  {
    this.rerender();
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('Saved As Draft'),
      detail: this.translateService.instant('Saved As Draft'),
      life: 6000,
    });

  })
}
ExportExcel(){
  this.ExportExcelFile.markAsTouched();
  if(this.ExportExcelFile.valid){
    this.workerService.downlode_file(this.LId , this.ExportExcelFile.value.year , this.ExportExcelFile.value.quarter).subscribe((res : Blob) =>
    {FileSaver.saveAs(res , "Worker List");},
)
  }
 
  }

 
  workersData = [];   

  onFileSelect(event) {
        let af = ['application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'application/vnd.ms-excel']
        if (event.target.files.length > 0) {
            const file = event.target.files[0];                    
            if (!_.includes(af, file.type)) {
                alert('Only EXCEL Docs Allowed!');
            } else {
                this.fileInputLabel = file.name;
                this.fileUploadForm.get('myfile').setValue(file);
            }
           }              
      }
                    
                    
  onFormSubmit() {
     if (!this.fileUploadForm.get('myfile').value) {
      this.app.messageService.add({
        severity: 'error',
        key: 'PlanValidation',
        summary: this.translateService.instant('Please fill valid details!'),
        detail: this.translateService.instant('Please fill valid details!'),
        life: 6000,
      });
          return false;
      }
      this.importExcelFile.markAllAsTouched();
      if(this.importExcelFile.valid){
       const formData = new FormData();
       formData.append('ColName', '1');
       formData.append('quarter', this.importExcelFile.value.quarter);
       formData.append('Remarks', this.importExcelFile.value.quarter);
       formData.append('year', this.importExcelFile.value.year);
       formData.append('formFile',  this.fileUploadForm.get('myfile').value );
       console.log(formData);
       this.workerService.importExcel(formData).subscribe((res) => {
       this.app.messageService.add({
          severity: 'success',
          key: 'PlanValidation',
          summary: this.translateService.instant('succes import file'),
          detail: this.translateService.instant('succes import file'),
          life: 6000,
        });
        console.log(res['data']);
        if(res['data'] != null){
           this.problemsImportActive = true;
           this.problemsImport = res['data'];
        }
        //    res['data'].forEach(element => {
        //   this.app.messageService.add({
        //     severity: 'error',
        //     key: 'PlanValidation',
        //     summary: this.translateService.instant('user Id : ' + element.userId + '<br>' + ' problem : '+element.problem),
        //     detail: this.translateService.instant('user Id : ' + element.userId + '<br>'+ 'problem : '+element.problem),
        //     life: 6000,
        //   });
        // })
       
        })
      }
      return false;
        
}
       
 emptyProblem(){
  this.problemsImport = [];
 }
  reset() {
    this.uploadProgress = null;
     this.uploadSub = null;
  }

  

}