import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { UserInfoServices } from '../../../services/userinfo.service';
import UserData from '../../../models/Common/userdata';
import { Response } from '../../../models/Common/response';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { Title } from "@angular/platform-browser";
import { DataTableDirective } from 'angular-datatables';
import { ClipboardService } from 'ngx-clipboard';
import { LovService } from '../../../services/Lov/lov.service';
import { CommonServices } from '../../../services/Common/common.service';
import { EntityServices } from '../../../services/Entity/entity.service';
import { FacilityServices } from '../../../services/Facility/facility.service';
import { LicenseServices } from '../../../services/License/License.service';
import { ActionCenterService } from '../../../services/ActionCenter/action-center.service';
import { DropDownType, SourceTypes } from '../../../Enumerations/Enums';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';
declare var $: any;

@Component({
  selector: 'trn-enquiry',
  templateUrl: './trn-enquiry.component.html',
  styleUrls: ['./trn-enquiry.component.css']
})

export class TransactionEnquiryComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  update = 0;
  selectedSource: any; currentTrnType: number;
  selectedTransactionTypeId: string;
  selectedSourceId: string;
  // Lookups and DropDowns

  sealedIds: string[] = [];
  unSealedIds: string[] = [];
  variableUnSealedIds: any[] = [];

  isSealedSelected: boolean = false;
  isUnSealedSelected: boolean = false;
  isVariableSealedSelected: boolean = false;


  entities: any[] = [];
  practices: any[] = [];
  facilities: any[] = [];
  licenses: any[] = [];
  permits: any[] = [];


  filteredEntities: any[] = [];
  filteredPractices: any[] = [];
  filteredFacilities: any[] = [];
  filteredLicenses: any[] = [];
  filteredPermits: any[] = [];



  selectedEntity: any[] = [];
  selectedFacility: any[] = [];
  selectedLicense: any[] = [];
  selectedPermit: any[] = [];
  selectedPractise: any[] = [];


  dropdownType: DropDownType = DropDownType.Entity;
  modalHeader: string = 'Entities Details'

  showLabel: boolean = false;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  preview: string = '';

  LovForm = new FormGroup({
    LovNameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    LovNameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    LovCode: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    SqlStatement: new FormControl('', [Validators.required, Validators.maxLength(2000), Validators.minLength(3)]),
  });
  dataList: any;
  historyList: any[] = [];

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private lovService: LovService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private commonService: CommonServices,
    private entityService: EntityServices,
    private facilityService: FacilityServices,
    private licenseService: LicenseServices,
    private actionCenterService: ActionCenterService,
    private titleService: Title,
    private clipboardApi: ClipboardService) {
    this.route.queryParams.subscribe(params => { });
  }


  dtInstanceObj: any;
  initialized: boolean = false;
  ngAfterViewInit(): void {

    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('Screen.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
    var me = this;
    this.dtTrigger.subscribe(() => {
      me.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
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

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });
    this.getUserData();
    // get entities
    this.entityService.GetAll().subscribe((res) => {
      this.entities = res['data'];
      this.filteredEntities = this.entities;


    });
    // get facilities
    this.facilityService.GetAll().subscribe((res) => {
      this.facilities = res['data'];
      this.filteredFacilities = this.facilities;

    });
    // get licenses
    this.licenseService.GetAll().subscribe((res) => {
      this.licenses = res['data'];
      this.filteredLicenses = this.licenses;


    });
    // get practices
    this.commonService.getPractiseProfile().subscribe((res) => {
      this.practices = res['data'];
      this.filteredPractices = this.practices;
    });
    // get permits
    this.commonService.getPermitProfile().subscribe((res) => {
      this.permits = res['data'];
      this.filteredPermits = this.permits;


    });

  }

  downloadFile(f: any) {
    const blob = new Blob([f.fileBytes], { type: f.contentType });
    FileSaver.saveAs(blob, f.fileOriginalName);
  }

  toggleSource(sourceType: SourceTypes, action: boolean) {
    if (sourceType == SourceTypes.sealed)
      this.isSealedSelected = !action
    if (sourceType == SourceTypes.unsealed)
      this.isUnSealedSelected = !action
    if (sourceType == SourceTypes.variableUnsealed)
      this.isVariableSealedSelected = !action

    this.rerender();
  }


  // Get Facilities by Entity
  getFacilitiesByEntity(isClear: boolean) {

    this.isSealedSelected = false;
    this.isUnSealedSelected = false;
    this.isVariableSealedSelected = false;


    if (isClear) {
      this.selectedEntity = [];
      this.filteredEntities = this.entities;
    }

    this.filteredFacilities = (this.selectedEntity && this.selectedEntity.length) > 0 ? this.facilities.filter(arr1 =>
      this.selectedEntity.find(arr2 => arr2.id === arr1.entityId)
    ) : this.facilities;


    this.filteredLicenses = (this.selectedFacility && this.selectedFacility.length) > 0 ? this.licenses.filter(arr1 =>
      this.selectedFacility.find(arr2 => arr2.id === arr1.facilityId)
    ) : this.licenses;


    this.filteredPermits = (this.selectedLicense && this.selectedLicense.length) > 0 ? this.permits.filter(arr1 =>
      this.selectedLicense.find(arr2 => arr2.id === arr1.licenseId)
    ) : this.permits;


    this.filteredPractices = (this.selectedPermit && this.selectedPermit.length) > 0 ? this.practices.filter(arr1 =>
      this.selectedPermit.find(arr2 => arr2.id === arr1.permitDetailsId)
    ) : this.practices;

    this.rerender();

  }


  //  Get Licenses by Facility
  getLicensesByFacility(isClear: boolean) {


    this.isSealedSelected = false;
    this.isUnSealedSelected = false;
    this.isVariableSealedSelected = false;


    if (isClear)
      this.selectedFacility = [];

    if (this.selectedFacility.length > 0) {
      this.selectedEntity = (this.filteredEntities && this.filteredEntities.length) > 0 ? this.filteredEntities.filter(arr1 =>
        this.selectedFacility.find(arr2 => arr2.entityId === arr1.id)
      ) : this.filteredEntities;

      this.getFacilitiesByEntity(false);
      this.filteredEntities = (this.selectedEntity && this.selectedEntity.length) > 0 ? this.selectedEntity : this.entities;

    }


    this.filteredLicenses = (this.selectedFacility && this.selectedFacility.length) > 0 ? this.licenses.filter(arr1 =>
      this.selectedFacility.find(arr2 => arr2.id === arr1.facilityId)
    ) : this.licenses;


    this.filteredPermits = (this.selectedLicense && this.selectedLicense.length) > 0 ? this.permits.filter(arr1 =>
      this.selectedLicense.find(arr2 => arr2.id === arr1.licenseId)
    ) : this.permits;


    this.filteredPractices = (this.selectedPermit && this.selectedPermit.length) > 0 ? this.practices.filter(arr1 =>
      this.selectedPermit.find(arr2 => arr2.id === arr1.permitDetailsId)
    ) : this.practices;


    this.rerender();

  }


  //  Get Permits by License
  getPermitsByLicense(isClear: boolean) {


    this.isSealedSelected = false;
    this.isUnSealedSelected = false;
    this.isVariableSealedSelected = false;

    if (isClear)
      this.selectedLicense = [];


    if (this.selectedLicense.length > 0) {
      this.selectedFacility = (this.filteredFacilities && this.filteredFacilities.length > 0) ? this.filteredFacilities.filter(arr1 =>
        this.selectedLicense.find(arr2 => arr2.facilityId === arr1.id)
      ) : this.filteredFacilities;

      this.getLicensesByFacility(false);
      this.filteredFacilities = (this.selectedFacility && this.selectedFacility.length > 0) ? this.selectedFacility : this.facilities;
    }

    this.filteredPermits = (this.selectedLicense && this.selectedLicense.length > 0) ? this.permits.filter(arr1 =>
      this.selectedLicense.find(arr2 => arr2.id === arr1.licenseId)
    ) : this.permits;


    this.filteredPractices = (this.selectedPermit && this.selectedPermit.length > 0) ? this.practices.filter(arr1 =>
      this.selectedPermit.find(arr2 => arr2.id === arr1.permitDetailsId)
    ) : this.practices;


    this.rerender();

  }


  //  Get Practises by Permit
  getPractisesByPermit(isClear: boolean) {


    this.isSealedSelected = false;
    this.isUnSealedSelected = false;
    this.isVariableSealedSelected = false;

    if (isClear)
      this.selectedPermit = [];

    if (this.selectedPermit.length > 0) {
      this.selectedLicense = (this.filteredLicenses.length && this.filteredLicenses.length > 0) ? this.filteredLicenses.filter(arr1 =>
        this.selectedPermit.find(arr2 => arr2.licenseId === arr1.id)
      ) : this.filteredLicenses;

      this.getPermitsByLicense(false);
      this.filteredLicenses = (this.selectedLicense && this.selectedLicense.length > 0) ? this.selectedLicense : this.licenses;
    }

    this.filteredPractices = (this.selectedPermit && this.selectedPermit.length > 0) ? this.practices.filter(arr1 =>
      this.selectedPermit.find(arr2 => arr2.id === arr1.permitDetailsId)
    ) : this.practices;


    this.rerender();

  }


  filterPractises(isClear: boolean) {


    this.isSealedSelected = false;
    this.isUnSealedSelected = false;
    this.isVariableSealedSelected = false;

    if (isClear)
      this.selectedLicense = [];

    this.selectedEntity = (this.filteredEntities.length && this.filteredEntities.length) > 0 ? this.filteredEntities.filter(arr1 =>
      this.selectedFacility.find(arr2 => arr2.entityId === arr1.id)
    ) : this.filteredEntities;

    this.getFacilitiesByEntity(false);
    this.filteredEntities = (this.selectedEntity && this.selectedEntity.length) > 0 ? this.selectedEntity : this.entities;


    this.selectedFacility = (this.filteredFacilities && this.filteredFacilities.length > 0) ? this.filteredFacilities.filter(arr1 =>
      this.selectedLicense.find(arr2 => arr2.facilityId === arr1.id)
    ) : this.filteredFacilities;

    this.getLicensesByFacility(false);
    this.filteredFacilities = (this.selectedFacility && this.selectedFacility.length) > 0 ? this.selectedFacility : this.facilities;



    this.selectedLicense = (this.filteredLicenses.length && this.filteredLicenses.length) > 0 ? this.filteredLicenses.filter(arr1 =>
      this.selectedPermit.find(arr2 => arr2.licenseId === arr1.id)
    ) : this.filteredLicenses;

    this.getPermitsByLicense(false);
    this.filteredLicenses = (this.selectedLicense && this.selectedLicense.length > 0) ? this.selectedLicense : this.licenses;


    this.selectedPermit = (this.filteredPermits && this.filteredPermits.length > 0) ? this.filteredPermits.filter(arr1 =>
      this.selectedPractise.find(arr2 => arr2.permitDetailsId === arr1.id)
    ) : this.filteredPermits;

    this.getPractisesByPermit(false);
    this.filteredPermits = (this.selectedPermit && this.selectedPermit.length > 0) ? this.selectedPermit : this.permits;


    this.rerender();

  }

  //// Get Licenses by Facility
  //getLicensesByFacility(isClear: boolean) {
  //  if (isClear)
  //    this.selectedEntity = [];
  //  this.filteredLicenses = this.filteredFacilities.length > 0 ? this.facilities.filter(arr1 =>
  //    this.selectedEntity.find(arr2 => arr2.id === arr1.entityId)
  //  ) : this.facilities;
  //}
  //// Get Permits by License
  //getPermitsByLicenseId(id: string) {
  //  //this.insertNewRecordService.getPermits(id).subscribe((res) => {
  //  //  this.permits = res['data'];
  //  //});
  //}



  getUserData(): void {
    var _this = this;
    _this.currentUser = _this.userService.getCurrentUser();
    _this.getData();
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
  OnlyEnglishCode(event: any, lng: any) {
    return this.commonClass.OnlyEnglishCode(event, lng);
  }

  getData() {

    var _this = this;
    _this.dataList = [];
    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 5,
      autoWidth: true,
      serverSide: true,
      searching: true,
      bLengthChange: false,
      ajax: (dataTablesParameters: any, callback, settings) => {

        dataTablesParameters.searchCriteria = _this._searchCriteria;
        dataTablesParameters.entities = (_this.selectedEntity == null || _this.selectedEntity.length == 0) ? null : _this.selectedEntity.map(({ id }) => id);
        dataTablesParameters.practices = (_this.selectedPractise == null || _this.selectedPractise.length == 0) ? null : _this.selectedPractise.map(({ id }) => id);
        dataTablesParameters.facilities = (_this.selectedFacility == null || _this.selectedFacility.length == 0) ? null : _this.selectedFacility.map(({ id }) => id);
        dataTablesParameters.licenses = (_this.selectedLicense == null || _this.selectedLicense.length == 0) ? null : _this.selectedLicense.map(({ id }) => id);
        dataTablesParameters.permits = (_this.selectedPermit == null || _this.selectedPermit.length == 0) ? null : _this.selectedPermit.map(({ id }) => id);
        dataTablesParameters.ids = [];

        if (_this.isSealedSelected)
          dataTablesParameters.ids.push.apply(dataTablesParameters.ids, this.sealedIds)
        if (_this.isUnSealedSelected)
          dataTablesParameters.ids.push.apply(dataTablesParameters.ids, this.unSealedIds)
        if (_this.isVariableSealedSelected)
          dataTablesParameters.ids.push.apply(dataTablesParameters.ids, this.variableUnSealedIds)

        dataTablesParameters.lang = _this.lang == 'en' ? 1 : 2;
        if (true) {
          _this.actionCenterService
            .GetAllEnquiryFunctional(dataTablesParameters)
            .subscribe((resp) => {
              if (resp['succeeded']) {
                _this.dtTrigger.next();
                _this.dataList = resp['data'];

                if (!_this.isSealedSelected && !_this.isUnSealedSelected && !_this.isVariableSealedSelected) {
                  _this.sealedIds = resp['sealedIds'];
                  _this.unSealedIds = resp['unSealedIds'];
                  _this.variableUnSealedIds = resp['variableUnSealedIds'];
                }

                callback({
                  recordsTotal: resp['recordsTotal'],
                  recordsFiltered: resp['recordsFiltered'],
                  data: resp['data'],
                });
              } else {
                _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + resp['errors'].join('=>'), life: 6000 });
              }
            });
        }
      },


      columns: [

        { data: 'nrrcId' },
        { data: 'manufacturerSerialNo' },
        { data: _this.lang == 'en' ? 'statusEn' : 'statusAr' },
        { data: 'radionuclideName' },
        { data: _this.lang == 'en' ? 'manufacturerEn' : 'manufacturerAr' },
        { data: 'productionDate' },
        { data: _this.lang == 'en' ? 'sourceTypeEn' : 'sourceTypeAr' },
      ]
    };

  }

  renderSearchTable(dropdownType: DropDownType) {
    $('#datatableexample').DataTable().destroy();
    this.dropdownType = dropdownType;
    this.modalHeader = dropdownType == DropDownType.Entity ? 'Entities Details' : dropdownType == DropDownType.Facility ? 'Facilities Details' :
      dropdownType == DropDownType.License ? "Licenses Details" : dropdownType == DropDownType.Permit ? 'Permits Details' : 'Practises Details';
    setTimeout(() => {
      $('#datatableexample').DataTable({
        pagingType: 'full_numbers',
        pageLength: 5,
        processing: true,
        destroy: true,
        lengthMenu: [5, 10, 25]
      });
    }, 1);
  }

  showLabelBox(show: boolean) {
    this.showLabel = show;
  }
  GetSourceById(id: string) {
    this.actionCenterService.GetSourceById(id).subscribe(res => {
      this.selectedSource = res;
      this.selectedSourceId = this.selectedSource.data.sourceId;
      this.currentTrnType = this.selectedSource.data.transactionId;
      this.selectedTransactionTypeId = this.selectedSource.data.transactionType;

    })
  }
  GetEditHistory(id: string, trnType: string) {
    this.actionCenterService.GetEditHistory(id, trnType).subscribe(res => {
      this.historyList = res["data"];
    })
  }

  Back() {
    this.router.navigate(['internal-user-settings']);
  }


  rerender(): void {
    this.dataList = [];

    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.ajax.reload()
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
  Home() {
    this.router.navigate(['aman-settings']);
  }


  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('datatableexample');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "Sheet.xlsx");
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessExportExcel'), detail: this.translateService.instant('General.SuccessExportExcel'), life: 6000 });

  }
  Print() {
    let element = document.getElementById('datatableexample');
    let newWin = window.open("");
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
}
