import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faListDots, faArrowAltCircleLeft, faAlignRight } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, Validators } from '@angular/forms';
import { AppComponent } from '../../app.component';
import { UserInfoServices } from '../../../services/userinfo.service';
import UserData from '../../../models/Common/userdata';
import { Response } from '../../../models/Common/response';
import BasCountries from '../../../models/Common/BasCoutries';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { Title } from "@angular/platform-browser";
import { DataTableDirective } from 'angular-datatables';

import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { ScreenService } from '../../../services/Screen/Screen.service';
import { ScreenField } from '../../../models/ScreenManagement/ScreenField';
declare var $: any;

@Component({
  selector: 'screen',
  templateUrl: './screen.component.html',
  styleUrls: ['./screen.component.css']
})

export class ScreenComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  screens = [];
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  update = 0;
  screenCode: string = '';
  screenNameAr: string = '';
  screenNameEn: string = '';
  screenId: string;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  preview: string = '';

  @Output() ScreenSelected = new EventEmitter<ScreenField[]>();

  @Input() isPreviewOnly = false;
  @Input() selectedScreenRole: string;


  ScreenForm = new FormGroup({
    ScreenNameAr: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    ScreenNameEn: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    ScreenCode: new FormControl('', [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private screenService: ScreenService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private titleService: Title,
    private clipboardApi: ClipboardService) {
    this.route.queryParams.subscribe(params => { });
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
      this.translateService.stream('Screen.PageTitle').subscribe(valueTitle => {
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
    _this.getAllScreens();
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
  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.screenCode,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {

        _this.screenService.DeleteScreen(obj.id).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded) {
            let index = _this.screens.findIndex(d => d.screenId === obj.screenId);
            _this.screens.splice(index, 1);
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
  getAllScreens() {
    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: this.isPreviewOnly == true?4:10,
      autoWidth: true,
      serverSide: true,
      searching: true,
      bLengthChange: this.isPreviewOnly == true ? false : true,

      ajax: (dataTablesParameters: any, callback, settings) => {
        dataTablesParameters.searchCriteria = this._searchCriteria;
        this.screenService
          .GetAllScreensFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              this.dtTrigger.next();
              this.screens = resp['data'];
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: resp['data'],
              });

              if (this.selectedScreenRole && this.isPreviewOnly == true) {
                this.ScreenClicked(this.selectedScreenRole);
              }


            } else {
              this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: this.translateService.instant('General.Error'), detail: this.translateService.instant('General.Error') + " " + resp['errors'].join('=>'), life: 6000 });
            }
          });
      },
      columns: this.isPreviewOnly == false ? [
        { data: 'screenNameAr' },
        { data: 'screenNameEn' },
        { data: 'screenCode' },
        { title: 'details', data: null },
        { title: 'actions', data: null }
      ] :
        [
          { data: 'screenNameAr' },
          { data: 'screenNameEn' },
          { data: 'screenCode' }
        ]

    };
  }
  reset() {
    this.update = 0;
    this.ScreenForm.reset();
  }
  Details(obj: any) {
    this.screenId = obj.id;
    this.screenCode = obj.screenCode;
    this.screenNameAr = obj.screenNameAr;
    this.screenNameEn = obj.screenNameEn;
    this.update = 1;
  }
  save() {
    var lang = this.translateService.currentLang;
    if (this.update == 0) {
      if (this.ScreenForm.valid) {
        this.preview = JSON.stringify(this.ScreenForm.value);
        var _this = this;
        _this.screenService.AddScreen(this.preview).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded) {
            _this.screens.push(response.data)
            _this.reset();
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
        "id": this.screenId,
        "screenCode": this.screenCode,
        "screenNameAr": this.screenNameAr,
        "screenNameEn": this.screenNameEn,
      }
      var _this = this;
      _this.screenService.UpdateScreen(obj).subscribe(function (data) {
        var response = data as Response<any>;
        if (response.succeeded) {

          _this.screens.map((obj) => {
            if (obj.screenId === response.data.screenId) {
              obj.screenCode = response.data.screenCode;
              obj.screenNameAr = response.data.screenNameAr;
              obj.screenNameEn = response.data.screenNameEn;
            }
          });
          _this.reset();
          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      })
    }
  }

  ScreenClicked(screenId: string) {
    if (this.isPreviewOnly == true) {
      var screenFields = this.screens.find(
        item => item.id === screenId).internalScreenFields;
      this.ScreenSelected.emit(screenFields);
    }
  }



  Back() {
    this.router.navigate(['internal-user-settings']);
  }


  // DataTable Methods
  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
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
  GotoScreenFieldPage(obj: any) {
    this.router.navigate(['screens-fields'], { queryParams: { Id: obj.id } });
  }
}
