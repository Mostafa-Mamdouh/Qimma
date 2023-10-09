import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  ViewChild,
  forwardRef,
} from '@angular/core';
import {
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
  faListDots,
  faTrash,
  faSearch,
  faCopy,
} from '@fortawesome/free-solid-svg-icons';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { TransactionService } from 'src/app/services/TransactionService/TransactionService.service';
import { SourceStatus, SourceTypes } from 'src/app/Enumerations/sourceTypes';
import { TransactionTypes } from 'src/app/Enumerations/TransactionTypes';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AppComponent } from 'src/app/app.component';
import { InsertNewRecordService } from 'src/app/services/Insert-New-Recored/insert-new-record.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  sourcesClicked: boolean = false;
  incidentsClicked: boolean = false;
  wasteClicked: boolean = false;
  entities: any[] = [];
  facilities: any[] = [];
  licenses?: any[] = [];

  currentEntity?: any;
  currentFacility?: any;
  currentLicense?: any;
  math = Math;

  isSources: boolean = true;
  isWaste: boolean = false;
  isIncidents: boolean = false;

  // Datatable
  transactionsList: any[] = [];
  dtOptions: DataTables.Settings = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  dtInstanceObj: any;
  initialized: boolean = false;
  SourceStatus = SourceStatus;
  SourceTypes = SourceTypes;
  TransactionTypes = TransactionTypes;
  trnStatus: number = -1;
  constructor(
    private sharedService: SharedService,
    private transactionService: TransactionService,
    private router: Router,
    private translateService: TranslateService,
    private insertNewRecordService: InsertNewRecordService,
    private cdr: ChangeDetectorRef,
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent
  ) {}

  faPlus = faPlus;
  faCopy = faCopy;
  faXmark = faXmark;
  faCircleUser = faCircleUser;
  faHouse = faHouse;
  faBrain = faBrain;
  faPersonBurst = faPersonBurst;
  faUsersLine = faUsersLine;
  faEnvelope = faEnvelope;
  faGlobe = faGlobe;
  faPen = faPen;
  faTrash = faTrash;
  faListDots = faListDots;
  faSearch = faSearch;
  lang: string;
  langServiceSupscribtion: Subscription;

  sourcesMenuClicked: boolean = false;

  radioactivity = (currentActivity) => {
    return currentActivity.toFixed(2) + ' Bq';
  };

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
                  setTimeout(() => {
                    var value = $(this).val();
                    if (that.search() !== value) {
                      that.search(value as string).draw();
                    }
                  }, 1000);
                });
              }
            });
          });
        }
      });
    });
  }
  ngOnInit() {
    // get entities
    this.insertNewRecordService.getEntities().subscribe((res) => {
      this.entities = res['data'];
      if (this.sharedService.currentEntity.value ) {
        this.currentEntity = this.sharedService.currentEntity.value;
        this.getFacilitiesByEntityId(this.currentEntity.id);
      }
    });

    console.log(this.currentEntity);
    console.log(this.currentFacility);
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
      this.getData();
    });
  }

  getData() {
    console.log('get data');
    const me = this;

    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: true,
      serverSide: true,
      searching: true,
      language: {
        paginate: {
          first: '<<',
          previous: '<',
          next: '>',
          last: '>>',
        },
        search: '',
        info:
          this.lang == 'ar'
            ? 'إظهار _START_ إلى _END_ من _TOTAL_ مدخلات'
            : 'Showing _START_ to _END_ of _TOTAL_ entries',
        lengthMenu: '_MENU_',
        emptyTable:
          this.lang == 'ar'
            ? 'لا يوجد بيانات متاحة في الجدول'
            : 'No data available in table',
        zeroRecords:
          this.lang == 'ar'
            ? 'لم يعثر على أية سجلات'
            : 'No matching records found',
        infoEmpty:
          this.lang == 'ar'
            ? 'إظهار 0 إلى 0 من أصل 0 مدخلات'
            : 'Showing 0 to 0 of 0 entries',
        processing: this.lang == 'ar' ? 'جارٍ المعالجة...' : 'Processing...',
      },
      ajax: (dataTablesParameters: any, callback, settings) => {
        dataTablesParameters.searchCriteria = me._searchCriteria;
        dataTablesParameters.lang = me.lang == 'en' ? 1 : 2;
        dataTablesParameters.data = me.trnStatus;
        dataTablesParameters.forPage = '';
        if (me.currentLicense) {
          dataTablesParameters.extra = me.currentLicense.id;
        }
        if (me.isIncidents) {
          dataTablesParameters.forPage = 'incident';
        } else if (me.isWaste) {
          dataTablesParameters.forPage = 'waste';
        }

        dataTablesParameters.order.push({ column: 3, dir: 'desc' });
        console.log('dt: ', dataTablesParameters);

        me.transactionService
          .getAllTransactionsFunctional(dataTablesParameters)
          .subscribe((resp) => {
            console.log('resp: ', resp);
            if (resp['succeeded']) {
              this.dtTrigger.next();
              me.transactionsList = resp['data'];
              console.log('transactionsList: ', me.transactionsList);
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: [],
              });
            } else {
              console.log("resp['errors']: ", resp['errors']);
            }
          });
      },
      columns:
        this.lang == 'en'
          ? [
              { data: 'sourceTypeEn' },
              { data: 'radionuclideName' },
              { data: 'radioActivity' },
              { data: 'manufacturerSerialNo' },
              { data: 'createdOn' },
              { data: 'statusEn' },
              { data: null },
            ]
          : [
              { data: 'sourceTypeAr' },
              { data: 'radionuclideName' },
              { data: 'radioActivity' },
              { data: 'manufacturerSerialNo' },
              { data: 'createdOn' },
              { data: 'statusAr' },
              { data: null },
            ],
    };
    this.rerender();
  }

  getFacilitiesByEntityId(id: string) {
    this.insertNewRecordService.getFacilities(id).subscribe((res) => {
      this.facilities = res['data'];
      this.sharedService.currentEntity.next(this.currentEntity);
      if (this.sharedService.currentFacility.value) {
        this.currentFacility = this.sharedService.currentFacility.value;
        this.getLicensesByFacilityId(this.currentFacility.id);
      }
    });
  }
  setFacility() {
    this.sharedService.currentFacility.next(this.currentFacility);
    this.getLicensesByFacilityId(this.currentFacility.id);
  }

  // Get Licenses by Facility
  getLicensesByFacilityId(id: string) {
    this.insertNewRecordService.getLicenses(id).subscribe((res) => {
      this.licenses = res['data'];
      if (this.sharedService.currentLicense.value) {
        this.currentLicense = this.sharedService.currentLicense.value;
        this.setLicense();
      }
    });
  }
  setLicense() {
    this.sharedService.currentLicense.next(this.currentLicense);
    this.reload();
  }

  GetListWithStatus() {
    this.reload();
  }
  reload() {
    this.getData();
    this.rerender();
  }

  rerender(): void {
    console.log('rerender');
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      console.log('rerender inside');
      this.cdr.detectChanges();
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

  handleSourcesMenuClick() {
    this.sourcesClicked = true;
    this.incidentsClicked = false;
    this.wasteClicked = false;
    this.isIncidents = false;
    this.isWaste = false;
    if (!this.isSources) {
      this.reload();
      this.isSources = true;
    }
  }

  handleIncidentsClick() {
    this.sourcesClicked = false;
    this.incidentsClicked = true;
    this.wasteClicked = false;
    this.isIncidents = true;
    this.isWaste = false;
    this.isSources = false;
    this.reload();
  }

  handleWasteClick() {
    this.sourcesClicked = false;
    this.incidentsClicked = false;
    this.wasteClicked = true;
    this.isIncidents = false;
    this.isWaste = true;
    this.isSources = false;
    this.reload();
  }

  UpdateTransaction(t: any) {
    let path = '';

    if (t.sourceType == SourceTypes.sealed) path = 'update-sealed-source';
    else if (t.sourceType == SourceTypes.unsealed)
      path = 'update-unsealed-source';
    else if (t.sourceType == SourceTypes.variableUnsealed)
      path = 'update-variable-unsealed-source';

    this.router.navigate([path], {
      queryParams: { id: t.id },
    });
  }

  UpdateSubmitedTransaction(t: any) {
    console.log('t: ', t);
    let path = '';

    if (t.sourceType == SourceTypes.sealed) path = 'edit-sealed-transaction';
    else if (t.sourceType == SourceTypes.unsealed)
      path = 'edit-unsealed-transaction';
    else if (t.sourceType == SourceTypes.variableUnsealed)
      path = 'edit-short-lived-unsealed-transaction';

    this.router.navigate([path], {
      queryParams: { id: t.id },
    });
  }
  Delete(t: any) {
    this.transactionService.DeleteDraft(t.id).subscribe((resp) => {
      if (resp['succeeded']) {
        this.app.messageService.add({
          severity: 'success',
          key: 'PlanValidation',
          summary: this.translateService.instant('Deleted Successfully'),
          detail: this.translateService.instant('Deleted Successfully'),
          life: 6000,
        });
        this.reload();
      }
    });
  }

  CreateSimilar(t: any) {
    this.transactionService.CreateSimiler(t.id).subscribe((resp) => {
      if (resp['succeeded']) this.reload();
    });
  }

  displaySelectEFPopup = false;
  navigateToAddSource(path: string) {
    if (this.currentEntity && this.currentFacility && this.currentLicense) {
      if(path == "worker"){
        this.router.navigate(['worker'] , 
        {queryParams : {E : this.currentEntity.id , F : this.currentFacility.id, L : this.currentLicense.id},
        });  
      }else{
        this.router.navigate([path]);  
      }
    } else {
      this.displaySelectEFPopup = true;
    }
  }

  viewRadionuclides(rads: any[]) {
    return rads.map((x) => x.isotope + ' ');
  }
}
