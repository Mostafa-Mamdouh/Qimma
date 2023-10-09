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
import { ManufacturerService } from '../../../services/ManufacturerMaster/Manufacturer.service';
import { DTOManufacturerMaster } from '../../../models/Manufacturer/manufacturerModel';
import BasCitesText from '../../../models/Common/BasCites.Text';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { DataTableDirective } from 'angular-datatables';


declare var $: any;

@Component({
  selector: 'app-practise-profile',
  templateUrl: './practise-profile.component.html',
  styleUrls: ['./practise-profile.component.css']
})

export class PractiseProfileComponent implements OnInit, AfterViewInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };
  currentUser: UserData = <UserData>{};
  DataObject: DTOManufacturerMaster = <DTOManufacturerMaster>{};
  DataList: any = [];


  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;


  SubmitForm = new FormGroup({

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
    private manufacturerService: ManufacturerService,
    private titleService: Title,
    private clipboardApi: ClipboardService
  ) {
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
          this.commonServices
            .getAllPractisesFunctional(dataTablesParameters)
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
          { data: 'practiseNameAr' },
          { data: 'practiseNameEn' },
          { data: 'permitDetailsId' },
          { data: 'amanInsertedOn' },
          { data: 'ammanId' },
        ],
      };
    //var _this = this;
    //_this.commonServices.getPractiseProfile().subscribe(function (data) {
    //  var response = data as Response<any>;
    //  if (response.succeeded) {
    //    _this.DataList = response.data;
        
    //    console.log(_this.DataList);
    //    setTimeout(() => {
    //      $('#lookUpsDataTable').DataTable({
    //        pagingType: 'full_numbers',
    //        pageLength: 10,
    //        processing: true,
    //        lengthMenu: [5, 10, 25],
            
    //      });
    //    }, 1);
    //  }
    //  else {
    //    _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
    //  }
    //})
  }

  ExportExel(): void {
      /* pass here the table id */
      let element = document.getElementById('lookUpsDataTable');
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      /* save to file */
      XLSX.writeFile(wb, "PractiseSheet.xlsx");
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
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessCopy'), detail: this.translateService.instant('General.SuccessCopy') , life: 6000 });
  }
  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }
  Back() {
    this.router.navigate(['aman-settings']);
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
  reload() {
    var _this = this;
    _this.getData();
    _this.rerender();
  }
}
