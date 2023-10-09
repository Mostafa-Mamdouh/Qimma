import {
  Component,
  forwardRef,
  Inject,
  ViewChild,
  ChangeDetectorRef,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  faTrash,
  faPenToSquare,
  faListDots,
  faArrowAltCircleLeft,
} from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { DataTableDirective } from 'angular-datatables';
import { ClipboardService } from 'ngx-clipboard';
import UserData from '../../../../models/Common/userdata';
import { AppComponent } from '../../../app.component';
import { UserInfoServices } from '../../../../services/userinfo.service';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { Response } from '../../../../models/Common/response';
import { ActionCenterService } from '../../../../services/ActionCenter/action-center.service';
import { CommonServices } from '../../../../services/Common/common.service';
import {
  ActionCenter,
  ItemSourceMsgHistory,
  ItemSourceStatus,
  ItemSourceStatusHistory,
  ItemSourceStatusHistoryEditor,
  TransactionTypeMaster,
} from '../../../../models/ActionCenter/action-center';
import { MailRequest } from '../../../../models/General/searchmultiproperty';
declare var $: any;
import * as FileSaver from 'file-saver';
import { Action } from 'rxjs/internal/scheduler/Action';
import { Base64ToPdf } from 'src/helper/fileToBase64';

@Component({
  selector: 'action-center',
  templateUrl: './action-center.component.html',
  styleUrls: ['./action-center.component.css'],
})
export class ActionCenterComponent implements OnInit {
  dtOptions = {};

  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  transctionTypeMasterId: string;
  mailMsg: string;
  currentUser: UserData = <UserData>{};
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;

  faTrash = faTrash;
  faPen = faPenToSquare;
  faListDots = faListDots;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  preview: string = '';
  dataList: ActionCenter[];

  transactionTypeList: TransactionTypeMaster[];
  sourceStatusList: ItemSourceStatus[];
  selectedTransactionTypeId: string;

  selectedSource: ActionCenter = new ActionCenter();
  shieldAttachments: any[] = [];
  sourceAttachments: any[] = [];

  selectedStatus: ItemSourceStatus = new ItemSourceStatus();
  selectedMsgHistory: ItemSourceMsgHistory[] = [];
  selectedStatusHistory: ItemSourceStatusHistory[] = [];
  selectedStatusId: string = '';
  remarks: string = '';

  historyList: any[] = [];
  currentTrnType: number = 1;

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private actionCenterService: ActionCenterService,
    private commonService: CommonServices,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private titleService: Title,
    private clipboardApi: ClipboardService
  ) {
    this.route.queryParams.subscribe((params) => {});
  }
  dtInstanceObj: any;
  initialized: boolean = false;
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe((value) => {
      this.translateService
        .stream('Screen.PageTitle')
        .subscribe((valueTitle) => {
          this.titleService.setTitle(value + valueTitle);
        });
    });
    var me = this;
    this.dtTrigger.subscribe(() => {
      me.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
        if (!this.initialized) {
          this.initialized = true;
          me.dtInstanceObj = dtInstance;
          console.log(this.dtElement);

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
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );

    var _this = this;
    this.getItemSourceStatus();
    this.getUserData();
    this.getTransactionTypeList();
  }

  downloadFile(f: any) {
    // const blob = new Blob([f.fileBytes], { type: f.contentType });
    let file = Base64ToPdf(f.fileBytes, f.fileOriginalName);
    FileSaver.saveAs(file, f.fileOriginalName);
  }

  getTransactions(type: any) {
    this.selectedTransactionTypeId = type.id;
    this.currentTrnType = type.transactionType;
    this.rerender();
  }

  getData() {
    var _this = this;
    this.dtOptions = {
      paging: true,
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: true,
      serverSide: true,
      searching: true,

      ajax: (dataTablesParameters: any, callback, settings) => {
        dataTablesParameters.searchCriteria = _this._searchCriteria;
        dataTablesParameters.id = _this.selectedTransactionTypeId;
        dataTablesParameters.lang = _this.lang == 'en' ? 1 : 2;

        if (_this.selectedTransactionTypeId) {
          _this.actionCenterService
            .GetAllActionsFunctional(dataTablesParameters)
            .subscribe((resp) => {
              if (resp['succeeded']) {
                _this.dtTrigger.next();
                _this.dataList = resp['data'];
                console.log(_this.dataList);
                callback({
                  recordsTotal: resp['recordsTotal'],
                  recordsFiltered: resp['recordsFiltered'],
                  data: resp['data'],
                });
              } else {
                _this.app.messageService.add({
                  severity: 'error',
                  key: 'PlanValidation',
                  summary: _this.translateService.instant('General.Error'),
                  detail:
                    _this.translateService.instant('General.Error') +
                    ' ' +
                    resp['errors'].join('=>'),
                  life: 6000,
                });
              }
            });
        }
      },
      columns: [
        {
          data: _this.lang == 'en' ? 'transactionTypeEn' : 'transactionTypeAr',
        },
        { data: 'transactionDate' },
        { data: _this.lang == 'en' ? 'entityEn' : 'entityAr' },
        { data: _this.lang == 'en' ? 'facilityEn' : 'facilityAr' },
        { data: _this.lang == 'en' ? 'licenseEn' : 'licenseAr' },
        { data: 'permitNumber' },
        { data: _this.lang == 'en' ? 'sourceTypeEn' : 'sourceTypeAr' },
        {
          data:
            _this.lang == 'en' ? 'transactionStatusEn' : 'transactionStatusAr',
        },
        { title: 'actions', data: null },
      ],
    };
  }
  getTransactionTypeList() {
    var _this = this;
    _this.commonService.getAllTransactionTypes().subscribe(function (data) {
      var response = data as Response<TransactionTypeMaster[]>;
      if (response.succeeded) {
        _this.transactionTypeList = response.data;
        console.log('types: ', _this.transactionTypeList);
        _this.selectedTransactionTypeId = _this.transactionTypeList[0].id;
        _this.rerender();
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

  addNewStatus() {
    var lang = this.translateService.currentLang;
    var editor = new ItemSourceStatusHistoryEditor();
    editor.statusId = this.selectedStatusId;
    editor.remarks = this.remarks;
    editor.sourceId = this.selectedSource.sourceId;
    editor.parentStatusId = this.selectedStatus.id;
    var _this = this;
    _this.actionCenterService
      .AddNewStatusHistory(editor)
      .subscribe(function (data) {
        var response = data as Response<any>;
        if (response.succeeded) {
          window.location.reload();
          _this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: _this.translateService.instant(
              'General.SavedSuccessfully'
            ),
            detail: _this.translateService.instant('General.SavedSuccessfully'),
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

  sendEmail() {
    var lang = this.translateService.currentLang;
    var editor = new MailRequest();
    editor.toEmail = 'saleem.a@qimmat-alkhaleej.com';
    editor.subject = 'Reminder';
    editor.body = this.mailMsg;
    editor.id = this.selectedSource.sourceId;
    var _this = this;
    _this.actionCenterService.SendEmail(editor).subscribe(function (data) {
      var response = data as Response<any>;
      if (response.succeeded) {
        window.location.reload();
        _this.app.messageService.add({
          severity: 'success',
          key: 'PlanValidation',
          summary: _this.translateService.instant('General.SavedSuccessfully'),
          detail: _this.translateService.instant('General.SavedSuccessfully'),
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
  GetSourceById(id: string) {}

  onOpenModal(source: ActionCenter, isView) {
    console.log(source);
    if (isView) {
      this.actionCenterService
        .GetProfileById(source.sourceId)
        .subscribe((res) => {
          this.sourceAttachments = res['data'].transactionAttactcments;
          console.log('attachment: ', this.sourceAttachments);
          this.shieldAttachments = res['data'].shieldAttachments;
        });
    }

    this.selectedSource = source;
    this.selectedStatus = this.sourceStatusList.filter((obj) => {
      return obj.statusNameEn === source.transactionStatusEn;
    })[0];
    this.selectedStatusId = this.selectedStatus.id;
    this.selectedStatusHistory = this.selectedSource.itemSourceStatusHistories;
    this.selectedMsgHistory = this.selectedSource.itemSourceMsgHistories;
    this.remarks = null;
  }
  getItemSourceStatus() {
    var _this = this;
    _this.commonService.getAllItemSourceStatus().subscribe(function (data) {
      var response = data as Response<ItemSourceStatus[]>;
      if (response.succeeded) {
        _this.sourceStatusList = response.data;
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

  getUserData(): void {
    var _this = this;

    _this.currentUser = _this.userService.getCurrentUser();
    this.getData();
  }

  Back() {
    this.router.navigate(['']);
  }
  // DataTable Methods
  rerender(): void {
    this.dataList = [];

    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
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
  Home() {
    this.router.navigate(['aman-settings']);
  }

  GetEditHistory(id: string, trnType: string) {
    console.log(id, trnType);
    this.actionCenterService.GetEditHistory(id, trnType).subscribe((res) => {
      console.log('history', res);
      this.historyList = res['data'];
    });
  }
}
