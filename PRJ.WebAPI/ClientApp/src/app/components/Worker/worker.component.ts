import { WorkerService } from 'src/app/services/Worker/worker.service';
import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';

import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faListDots,faPen,faGlobe, faPlus, faXmark ,faCircleUser,faHouse, faBrain,faUsersLine,faPersonBurst,faEnvelope} from '@fortawesome/free-solid-svg-icons';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { DataTableDirective } from 'angular-datatables';
// import { DataTableDirective } from 'angular-datatables';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexYAxis,
  ApexXAxis,
} from "ng-apexcharts";
import { ActivatedRoute, Router } from '@angular/router';
import { InsertNewRecordService } from 'src/app/services/Insert-New-Recored/insert-new-record.service';
import { DTOWorkers } from 'src/app/models/Worker/worker';
import { encode } from 'punycode';
import { PractiseProfileService } from 'src/app/services/PractiseProfile/practise-profile.service';
export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
};
@Component({
  selector: 'app-worker',
  templateUrl: 'worker.component.html',
  styleUrls: ['./worker.component.css']
})
export class WorkerComponent implements OnInit {
  constructor(     
    private sharedService: SharedService,
    private workerService :WorkerService,
    private route: ActivatedRoute,
    private router:Router,
    private fb: FormBuilder,
    private insertNewRecordService: InsertNewRecordService,
    private PractiseProfile: PractiseProfileService,


    ) { }
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  faPlus = faPlus;
  faXmark = faXmark;
  faCircleUser =faCircleUser;
  faHouse=faHouse;
  faBrain=faBrain;
  faPersonBurst=faPersonBurst;
  faUsersLine=faUsersLine;
  faEnvelope=faEnvelope;
  faGlobe=faGlobe;
  faPen = faPen;
  faListDots =faListDots;
  lang: string;
  langServiceSupscribtion: Subscription;
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  Facility:any;
  entities: any[] = [];
  facilities: any[] = [];
  licenses?: any[] = [];
  objData: DTOWorkers = <DTOWorkers>{
    'id' : '',
    'Facility': '',
    'License' : '',
    'Entity': '',
    'Practise' : '',
  };
  currentEntity?: any;
  currentFacility?: any;
  currentLicense?: any;
  math = Math;
  EntityList : any;
  LicenseList:any;
  dataList: any[] = [];
  PractiseList : any ;
  lookupsList?: any = [];
  Facilities?: any = [];
  License : any;
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  FId : string;
  EId : string; 
  LId : string; 

  radionuclides: any = [];
  
  ngOnInit(): void {    
    this.route.queryParams.subscribe((params) => {
     params['id'];
     this.FId = params['F']
     this.EId = params['E']
     this.LId = params['L']
    });
    this.getHeadeDataById();
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
      // this.updateOptions();
    });
    this.License = this.LId;
    this.getdata();
    this.insertNewRecordService.getEntities().subscribe((res) => {
      this.entities = res['data'];
      this.getFacilitiesByEntityId(this.EId);
    });
  }
  getHeadeDataById(){
    this.workerService.getEntityById(this.EId).subscribe((res) => {
     this.currentEntity = res['data'];
    })
    this.workerService.getFacilityById(this.FId).subscribe((res) => {
      this.currentFacility = res['data'];
    })
     this.workerService.getLicenseById(this.LId).subscribe((res) => {
      this.currentLicense = res['data'];
    });
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
      ajax: (dataTablesParameters: any, callback, settings) => {
        console.log('dataTablesParameters:', dataTablesParameters);
        dataTablesParameters.searchCriteria = this._searchCriteria;
        dataTablesParameters.extra = this.License;
        this.workerService
          .getWorkerFunctional(dataTablesParameters)
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
      columns: [
        { data: 'workerNameEn'},
        { data: 'nationalityId'},
        { data: 'gender' },
        { data: 'lastWorkersExposuresDose'},
        { data: null },
        { data: null },
        { title:'actions', data: null }
      ],
    };
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

  GoToAddworker(){
    this.router.navigate(['/Addworker'],
    {queryParams : {E : this.currentEntity.id , F : this.currentFacility.id, L : this.currentLicense.id},
  });
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


  GoToDetails(id:any){
    this.router.navigate(['/workerDetails'], 
    {queryParams : {id: id , E : this.currentEntity.id , F : this.currentFacility.id, L : this.currentLicense.id},
  })}

  GoToMassUpdate(){
    this.router.navigate(['/worker-Mass-Update'],
    {queryParams : {E : this.currentEntity.id , F : this.currentFacility.id, L : this.currentLicense.id},
  });
  }

  //////////////////////////
  setFacility() {
    this.sharedService.currentFacility.next(this.currentFacility);
    this.getLicensesByFacilityId(this.currentFacility.id);
  }

  getFacilitiesByEntityId(id: string) {
    this.insertNewRecordService.getFacilities(id).subscribe((res) => {
      this.facilities = res['data'];
      this.sharedService.currentEntity.next(this.currentEntity);
      this.getLicensesByFacilityId(this.FId);
    });
  }

  // Get Licenses by Facility
  getLicensesByFacilityId(id: string) {
    this.insertNewRecordService.getLicenses(id).subscribe((res) => {
        this.licenses = res['data'];
        //this.currentLicense = this.currentLicense;
        this.sharedService.currentLicense.next(this.currentLicense);
        
    });
  }
  setLicense() {
    this.sharedService.currentLicense.next(this.currentLicense);

  }


}

