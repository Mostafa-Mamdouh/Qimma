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
import { CommonServices } from '../../../services/Common/common.service';
import { DataTableDirective } from 'angular-datatables';

import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
declare var $: any;

@Component({
  selector: 'bas-countries',
  templateUrl: './bascountries.component.html',
  styleUrls: ['./bascountries.component.css']
})

export class BasCountriesComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  BasCountries = [];
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  CountryCodeISO: string = '';
  CountryNameAr: string = '';
  CountryNameEn: string = '';
  NationalityNameFrn: string = '';
  NationalityNameNtv: string = '';
  id: string;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  preview: string = '';

  CountriesForm = new FormGroup({
    CountryNameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    CountryNameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    NationalityNameFrn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    NationalityNameNtv: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    CountryCodeISO: new FormControl('', [Validators.required, Validators.maxLength(3), Validators.minLength(2), Validators.pattern('^[a-zA-Z]+$')]),
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
    { });
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
    _this.getAllBasCountries();
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

        _this.commonServices.DeleteCountry(obj.id).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded) {
            //_this.getAllBasCountries();
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
  getAllBasCountries() {
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
        this.commonServices
          .GetAllCountriesFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log(resp);
              this.dtTrigger.next();
              this.BasCountries = resp['data'];
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
        { data: 'countryNameAr' },
        { data: 'countryNameEn' },
        { data: 'nationalityNameFrn' },
        { data: 'nationalityNameNtv' },
        { data: 'countryCodeISO' },
        { data: null },
        { data: null }
      ],
    };
  }
  reset() {
    this.Update = 0;
    this.CountriesForm.reset();
  }
  Details(obj: any) {
   
    this.id = obj.id;
    this.CountryCodeISO = obj.countryCodeISO;
    this.CountryNameAr = obj.countryNameAr;
    this.CountryNameEn = obj.countryNameEn;
    this.NationalityNameFrn = obj.nationalityNameFrn;
    this.NationalityNameNtv = obj.nationalityNameNtv;
    this.Update = 1;
  }
  save() {
    var lang = this.translateService.currentLang;
    if (this.Update == 0) {
      if (this.CountriesForm.valid) {
        this.preview = JSON.stringify(this.CountriesForm.value);
        var _this = this;
        _this.commonServices.AddCountry(this.preview).subscribe(function (data) {
          var response = data as Response<BasCountries>;
          if (response.succeeded) {
            _this.BasCountries = <any> response.data;
            //_this.getAllBasCountries();
            _this.reload();
            _this.CountriesForm.reset();
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
      var obj = {
        "Id": this.id,
        "countryCodeISO": this.CountryCodeISO,
        "countryNameAr": this.CountryNameAr, 
        "countryNameEn": this.CountryNameEn ,
        "nationalityNameFrn": this.NationalityNameFrn,
        "nationalityNameNtv": this.NationalityNameNtv ,
      }
      var _this = this;
      _this.commonServices.UpdateCountry(obj).subscribe(function (data) {
        var response = data as Response<BasCountries>;
        if (response.succeeded) {
          //_this.getAllBasCountries();
          _this.reload();
          _this.CountriesForm.reset();
          _this.Update = 0;
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      })
    }
  }
  CreateCites(obj: any) {
    this.router.navigate(['bas-cites'], { queryParams: { Id: obj.id } });
  }
  Back() {
    this.router.navigate(['system-settings']);
  }

  // DataTable Methods
  reload() {
    var _this = this;
    _this.getAllBasCountries();
    _this.rerender();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
      dtInstance.ajax.reload();
    });
  }

  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('lookUpsDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "CounrtySheet.xlsx");
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

  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }

}
