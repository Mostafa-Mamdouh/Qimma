import {
  Component,
  forwardRef,
  Inject,
  ViewChild,
  ChangeDetectorRef,
  AfterViewInit,
} from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  faTrash,
  faPenToSquare,
  faArrowAltCircleLeft,
} from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AppComponent } from '../../../app.component';
import UserData from '../../../../models/Common/userdata';
import { Response } from '../../../../models/Common/response';
import BasCountries from '../../../../models/Common/BasCoutries';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from '@angular/platform-browser';
import { CommonServices } from '../../../../services/Common/common.service';
import { LookupSet } from '../../../../models/LookupSet/LookupSet';
import { UserInfoServices } from '../../../../services/userinfo.service';
import { LookupSetTerm } from '../../../../models/LookupSetTerm/LookupSetTerm';
import { DataTableDirective } from 'angular-datatables';
import { param } from 'jquery';

declare var $: any;

@Component({
  selector: 'lookup-set-term',
  templateUrl: './lookupset-term.component.html',
  styleUrls: ['./lookupset-term.component.css'],
})
export class LookupSetTermComponent implements OnInit, AfterViewInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  objData: LookupSetTerm = <LookupSetTerm>{};
  dataList: any = [];
  lookUpObj: any;
  ParamId: string = '';
  className: string = '';

  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;

  SubmitForm = new FormGroup({
    Value: new FormControl('', [
      Validators.required,
      Validators.maxLength(200),
      Validators.minLength(1),
    ]),
    DisplayNameAr: new FormControl('', [
      Validators.required,
      Validators.maxLength(200),
    ]),
    DisplayNameEn: new FormControl('', [
      Validators.required,
      Validators.maxLength(200),
    ]),
    ExtraData1: new FormControl('', [Validators.maxLength(200)]),
    ExtraData2: new FormControl('', [Validators.maxLength(200)]),
    IsActive: new FormControl(false, [Validators.required]),
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
    private titleService: Title
  ) {
    this.route.queryParams.subscribe((params) => {
      this.ParamId = params['Id'];
      this.className = params['class'];
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
    this.translateService.stream('ApplicationTitle').subscribe((value) => {
      this.translateService
        .stream('LookUps.PageTitle')
        .subscribe((valueTitle) => {
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
    _this.currentUser = _this.userService.getCurrentUser();
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
        dataTablesParameters.extra = this.ParamId;
        this.commonServices
          .getAllLookupsTermsFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log(resp);
              this.dtTrigger.next();
              this.dataList = resp['data'];
              callback({
                recordsTotal: resp['recordsTotal'],
                recordsFiltered: resp['recordsFiltered'],
                data: resp['data'],
              });
            } else {
              this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: this.translateService.instant('General.Error'),
                detail:
                  this.translateService.instant('General.Error') +
                  ' ' +
                  resp['errors'].join('=>'),
                life: 6000,
              });
            }
          });
      },
      columns: [
        { data: 'displayNameAr' },
        { data: 'displayNameEn' },
        { data: 'value' },
        { data: null },
      ],
    };
    //var _this = this;

    //_this.commonServices.getLookupById(_this.ParamId).subscribe(function (data) {
    //  var responseCountry = data as Response<LookupSet>;

    //  if (responseCountry.succeeded) {

    //    _this.lookUpObj = responseCountry.data;

    //    _this.commonServices.getLookupsBySetId(_this.ParamId).subscribe(function (data) {

    //      var response = data as Response<LookupSetTerm[]>;
    //      if (response.succeeded) {

    //        _this.dataList = response.data;
    //        //console.log(_this.dataList);
    //        setTimeout(() => {
    //          $('#datatableexample').DataTable({
    //            pagingType: 'full_numbers',
    //            pageLength: 5,
    //            processing: true,
    //            lengthMenu: [5, 10, 25]
    //          });
    //        }, 1);
    //      }
    //      else {
    //        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
    //      }

    //    })
    //  }
    //  else {
    //    _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + responseCountry.errors.join('=>'), life: 6000 });
    //  }
    //})
  }

  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message:
        _this.translateService.instant('Confirmation.deleteMsg') +
        ': ' +
        obj.className,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        _this.commonServices
          .deleteLookUpTerm(obj.id)
          .subscribe(function (data) {
            var response = data as Response<boolean>;
            if (response.succeeded && response.data) {
              _this.reload();
              _this.app.messageService.add({
                severity: 'success',
                key: 'PlanValidation',
                summary: _this.translateService.instant(
                  'General.DeletedSuccessfully'
                ),
                detail: _this.translateService.instant(
                  'General.DeletedSuccessfully'
                ),
                life: 6000,
              });
            } else if (
              response.succeeded == false &&
              response.message == 'NOTEXIST'
            ) {
              _this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: _this.translateService.instant('General.NotFound'),
                detail: this.translateService.instant('General.NotFound'),
                life: 6000,
              });
            } else {
              _this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: _this.translateService.instant('General.Error'),
                detail:
                  _this.translateService.instant('General.Error') +
                  ' ' +
                  response.errors.join('=>'),
                life: 6000,
              });
            }
          });
      },
      reject: () => {
        _this.app.messageService.add({
          severity: 'warn',
          key: 'PlanValidation',
          summary: _this.translateService.instant('General.DeleteCancelled'),
          detail: _this.translateService.instant('General.DeleteCancelled'),
          life: 6000,
        });
      },
    });
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
    this.objData.id = obj.id;
    this.objData.value = obj.value;
    this.objData.displayNameAr = obj.displayNameAr;
    this.objData.displayNameEn = obj.displayNameEn;
    this.objData.extraData1 = obj.extraData1;
    this.objData.extraData2 = obj.extraData2;
    this.objData.isActive = obj.isActive;

    this.Update = 1;
  }
  save() {
    var _this = this;

    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      if (_this.SubmitForm.valid) {
        var objSave = {
          Id: _this.objData.id,
          LookupSetId: _this.ParamId,
          Value: _this.SubmitForm.controls['Value'].value,
          DisplayNameAr: _this.SubmitForm.controls['DisplayNameAr'].value,
          DisplayNameEn: _this.SubmitForm.controls['DisplayNameEn'].value,
          ExtraData1: _this.SubmitForm.controls['ExtraData1'].value,
          ExtraData2: _this.SubmitForm.controls['ExtraData2'].value,
          IsActive: _this.SubmitForm.controls['IsActive'].value,
          ModifiedBy: _this.currentUser.username,
          CreatedBy: _this.currentUser.username,
        };
        _this.commonServices
          .addLookupSetTerm(objSave)
          .subscribe(function (data) {
            var response = data as Response<boolean>;
            if (response.succeeded && response.data) {
              _this.reload();
              _this.SubmitForm.reset();
              _this.app.messageService.add({
                severity: 'success',
                key: 'PlanValidation',
                summary: _this.translateService.instant(
                  'General.SavedSuccessfully'
                ),
                detail: _this.translateService.instant(
                  'General.SavedSuccessfully'
                ),
                life: 6000,
              });
            } else {
              _this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: _this.translateService.instant('General.Error'),
                detail:
                  _this.translateService.instant('General.Error') +
                  ' ' +
                  response.errors.join('=>'),
                life: 6000,
              });
            }
          });
      } else {
        this.app.messageService.add({
          severity: 'error',
          key: 'PlanValidation',
          summary: this.translateService.instant('General.RequiredAllFields'),
          detail: this.translateService.instant('General.RequiredAllFields'),
          life: 6000,
        });
      }
    } else {
      var objUpdate = {
        LookupSetTermId: _this.objData.id,
        LookupSetId: _this.ParamId,
        Value: _this.objData.value,
        DisplayNameAr: _this.objData.displayNameAr,
        DisplayNameEn: _this.objData.displayNameEn,
        ExtraData1: _this.objData.extraData1,
        ExtraData2: _this.objData.extraData2,
        IsActive: _this.objData.isActive,
        ModifiedBy: _this.currentUser.username,
      };
      console.log(objUpdate);
      var _this = this;
      _this.commonServices
        .updateLookupSetTerm(objUpdate)
        .subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.reload();
            _this.SubmitForm.reset();
            _this.Update = 0;
            _this.app.messageService.add({
              severity: 'success',
              key: 'PlanValidation',
              summary: _this.translateService.instant(
                'General.SavedSuccessfully'
              ),
              detail: _this.translateService.instant(
                'General.SavedSuccessfully'
              ),
              life: 6000,
            });
          } else if (
            response.succeeded == false &&
            response.message == 'NOTEXIST'
          ) {
            _this.app.messageService.add({
              severity: 'error',
              key: 'PlanValidation',
              summary: _this.translateService.instant('General.NotFound'),
              detail: _this.translateService.instant('General.NotFound'),
              life: 6000,
            });
          } else {
            _this.app.messageService.add({
              severity: 'error',
              key: 'PlanValidation',
              summary: _this.translateService.instant('General.Error'),
              detail:
                _this.translateService.instant('General.Error') +
                ' ' +
                response.errors.join('=>'),
              life: 6000,
            });
          }
        });
    }
  }
  Back() {
    this.router.navigate(['lookup-set']);
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
  // DataTable Methods
  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
      dtInstance.ajax.reload();
    });
  }
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
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
