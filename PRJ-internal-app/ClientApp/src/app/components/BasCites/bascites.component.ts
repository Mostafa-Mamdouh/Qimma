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
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from "@angular/platform-browser";
import { BasCites } from '../../../models/Common/BasCites';
import { CommonServices } from '../../../services/Common/common.service';

import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { DataTableDirective } from 'angular-datatables';
declare var $: any;

@Component({
  selector: 'bas-cites',
  templateUrl: './bascites.component.html',
  styleUrls: ['./bascites.component.css']
})

export class BasCitesComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  cityObj: BasCites = <BasCites>{};
  countryObj: any;
  CountryId: string = "";
  BasCitesList: any = [];
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  preview: string = '';

  CitesForm = new FormGroup({
    NameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    NameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    CityAbbrCode: new FormControl('', [Validators.required, Validators.maxLength(7), Validators.minLength(3)]),
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
    private titleService: Title,
    private clipboardApi: ClipboardService  ) {
    this.route.queryParams.subscribe(params =>
    {
      this.CountryId = params["Id"];
    });
  }
  
  ngOnInit(): void {
    this.getCountry();
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
    
  _this.userService.getCurrentUser();
    _this.getCites();

  }
  getCountry() {
    var _this = this;
    _this.commonServices.getCountryById(_this.CountryId).subscribe(function (data) {
      var responseCountry = data as Response<BasCountries>;

      if (responseCountry.succeeded) {

        _this.countryObj = responseCountry.data;

        console.log(_this.countryObj);
      }
    })
  }
  getCites() {
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
        dataTablesParameters.extra = this.CountryId;
        this.commonServices
          .getAllCitiesFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log(resp);
              this.dtTrigger.next();
              this.BasCitesList = resp['data'];
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
        { data: 'nameAr' },
        { data: 'nameEn' },
        { data: 'cityAbbrCode' },
        { data: null }
      ],
    }
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
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.countryCodeISO,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {

        _this.commonServices.deleteCity(obj.id).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.DeletedSuccessfully'), detail: _this.translateService.instant('General.DeletedSuccessfully'), life: 6000 });
          }
          else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error')  + " " + response.errors.join('=>'), life: 6000 });
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
    _this.getCites();
    _this.rerender();
  }
  reset() {
    this.Update = 0;
    this.CitesForm.reset();
  }
  Details(obj: any) {
   
    this.cityObj.id = obj.id;
    this.cityObj.countryId = obj.countryId
    this.cityObj.nameAr = obj.nameAr;
    this.cityObj.nameEn = obj.nameEn;
    this.cityObj.cityAbbrCode = obj.cityAbbrCode;
    this.Update = 1;
  }
  save() {
    var _this = this;

    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      if (_this.CitesForm.valid) {
        var objSave = {
          "CountryId": _this.CountryId,
          "NameAr": _this.CitesForm.controls["NameAr"].value,
          "NameEn": _this.CitesForm.controls["NameEn"].value,
          "CreatedBy": _this.currentUser.username,
          "ModifiedBy": _this.currentUser.username,
          "CityAbbrCode": _this.CitesForm.controls["CityAbbrCode"].value,
        };
        
        _this.commonServices.addCity(objSave).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.CitesForm.reset();
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
      var objUpdate = {
        "Id": _this.cityObj.id,
        "CountryId": _this.CountryId,
        "NameAr": _this.cityObj.nameAr,
        "NameEn": _this.cityObj.nameEn,
        "CreatedBy": _this.currentUser.username,
        "ModifiedBy": _this.currentUser.username, 
        "CityAbbrCode": _this.cityObj.cityAbbrCode
      };
      var _this = this;
      _this.commonServices.updateCity(objUpdate).subscribe(function (data) {
        var response = data as Response<boolean>;
        if (response.succeeded && response.data) {
          _this.reload();
          _this.CitesForm.reset();
          _this.Update = 0;
          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      })
    }
  }
  Back() {
    this.router.navigate(['bas-countries']);
  }
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }

  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('CitiesDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "CitiesSheet.xlsx");
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessExportExcel'), detail: this.translateService.instant('General.SuccessExportExcel'), life: 6000 });

  }
  Print() {
    let element = document.getElementById('CitiesDataTable');
    let newWin = window.open("");
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('CitiesDataTable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessCopy'), detail: this.translateService.instant('General.SuccessCopy'), life: 6000 });
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
