import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { UserInfoServices } from '../../../services/userinfo.service';
import UserData from '../../../models/Common/userdata';
import { Response } from '../../../models/Common/response';
import BasCountries from '../../../models/Common/BasCoutries';
import BasCoutriesText from '../../../models/Common/BasCoutries.Text';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from "@angular/platform-browser";
import { CommonServices } from '../../../services/Common/common.service';
import { EntityServices } from '../../../services/Entity/entity.service';

import BasCitesText from '../../../models/Common/BasCites.Text';
import { DTOFacility } from '../../../models/Facility/facility';
import { FacilityServices } from '../../../services/Facility/facility.service';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { DataTableDirective } from 'angular-datatables';

declare var $: any;

@Component({
  selector: 'Facility',
  templateUrl: './Facility.component.html',
  styleUrls: ['./Facility.component.css']
})

export class FacilityComponent implements OnInit, AfterViewInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  DataObject: DTOFacility = <DTOFacility>{};
  DataList: any = [];
  CountryList: any;
  CityList: any;

  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;


  SubmitForm = new FormGroup({
    FacilityNameAr : new FormControl (''),
    FacilityNameEn : new FormControl(''),
    OragnizationName: new FormControl(''),
    FacilityCode: new FormControl(''),
    Province: new FormControl(''),
    City: new FormControl(''),
    Location: new FormControl(''),
    EntityId: new FormControl(''),
    AmanInsertedOn: new FormControl(''),
    AmanID: new FormControl(''),
    FacilitySource: new FormControl(''),

  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private commonServices: CommonServices,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private sharedService: SharedService,
    private ConfirmationService: ConfirmationService,
    private MessageService: MessageService,
    private commonClass: CommonClass,
    private facilityServices: FacilityServices,
    private titleService: Title,
    private clipboardApi: ClipboardService ) {
    this.route.queryParams.subscribe(params => {

    });
  }

  ngOnInit(): void {
    this.getUserData();

    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  dtInstanceObj: any;
  initialized: boolean = false;
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('BasCountries.PageTitle').subscribe(valueTitle => {
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
        this.facilityServices
          .GetAllFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log(resp);
              this.dtTrigger.next();
              this.DataList = resp['data'];
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
        { data: 'facilityNameAr' },
        { data: 'facilityNameEn' },
        { data: 'facilityCode' },
        { data: 'amanInsertedOn' },
        { data: null }
      ],
    };
  }
  getCountries() {
    var _this = this;
    _this.commonServices.getCountriesClear().subscribe(function (data) {
      var response = data as Response<BasCoutriesText[]>;
      if (response.succeeded) {
        _this.CountryList = response.data;
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }
  
  /*getCites(Id: number, selectedCity: number) {
   
    var _this = this;
    _this.commonServices.getCitesByCountryIdClear(Id).subscribe(function (data) {
      var response = data as Response<BasCitesText[]>;
      if (response.succeeded) {
        _this.CityList = response.data;

        if (selectedCity > 0) {
         
          _this.DataObject.City = selectedCity;
        }
        
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }*/

  disable(evt: any) {
    return false;
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
  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.manufacturerDescAr,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {

        _this.facilityServices.Delete(obj.id).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.DeletedSuccessfully'), detail: _this.translateService.instant('General.DeletedSuccessfully'), life: 6000 });
          }
          else {
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
    var _this = this;
    _this.DataObject = <DTOFacility>{};
    _this.DataObject.id = obj.id;
    _this.DataObject.AmanID = obj.amanID;
    _this.DataObject.AmanInsertedOn = obj.amanInsertedOn;
    _this.DataObject.FacilityCode = obj.facilityCode;
    _this.DataObject.FacilityNameAr = obj.facilityNameAr
    _this.DataObject.FacilityNameEn = obj.facilityNameEn
    _this.DataObject.FacilitySource = obj.facilitySource
    _this.DataObject.Location = obj.location
    _this.DataObject.City = obj.city
    _this.DataObject.OragnizationName = obj.oragnizationName
    _this.DataObject.Province = obj.province
    _this.DataObject.FacilitySource = obj.facilitySource
    _this.Update = 1;
  }
  save() {
    var _this = this;

    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      if (_this.SubmitForm.valid) {
        _this.facilityServices.Add(JSON.stringify(this.SubmitForm.value)).subscribe(function (data) {
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
      else {
        this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.RequiredAllFields'), detail: this.translateService.instant('General.RequiredAllFields'), life: 6000 });
      }
    }
    else {

      var _this = this;
      this.SubmitForm.controls['Id'].setValue(_this.DataObject.id);


      _this.facilityServices.Update(JSON.stringify(this.SubmitForm.value)).subscribe(function (data) {
        var response = data as Response<boolean>;
        if (response.succeeded && response.data) {
          _this.reload();
          _this.SubmitForm.reset();
          _this.Update = 0;
          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      })
    }
  }
  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('lookUpsDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "facilitySheet.xlsx");
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessExportExcel'), detail: this.translateService.instant('General.SuccessExportExcel'), life: 6000 });

  }
  Print() {
    let element = document.getElementById('lookUpsDataTable');
    let newWin = window.open("");
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('lookUpsDataTable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessCopy'), detail: this.translateService.instant('General.SuccessCopy'), life: 6000 });
  }

  Back() {
    this.router.navigate(['aman-settings']);
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
}
