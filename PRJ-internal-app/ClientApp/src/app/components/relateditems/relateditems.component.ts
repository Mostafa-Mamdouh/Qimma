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
  faEye,
} from '@fortawesome/free-solid-svg-icons';
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
import { Title } from '@angular/platform-browser';
import { CommonServices } from '../../../services/Common/common.service';
import { DTORelatedItems } from '../../../models/RelatedItems/relateditemsModel';
import BasCitesText from '../../../models/Common/BasCites.Text';
import { DataTableDirective } from 'angular-datatables';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { ManufacturerService } from 'src/services/ManufacturerMaster/Manufacturer.service';
import { RelatedItemsService } from '../../../services/RelatedItems/relatedItems.service';

@Component({
  selector: 'app-relateditems',
  templateUrl: './relateditems.component.html',
  styleUrls: ['./relateditems.component.css'],
})
export class RelateditemsComponent implements OnInit {
  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  DataObject: DTORelatedItems = <DTORelatedItems>{};
  DataList: any[] = [];
  CountryList: any;
  CityList: any;

  NuclearEntity?: any = [];
  ItemCategory?: any = [];
  Manufacturer?: any = [];

  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  faEye = faEye;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;

  dtInstanceObj: any;
  initialized: boolean = false;

  SubmitForm = new FormGroup({
    RelatedItemCode: new FormControl(''),
    StatusID: new FormControl(6),
    Description: new FormControl('', [
      Validators.required,
      Validators.maxLength(200),
    ]),
    ManufacturerSerialNom: new FormControl('', [
      Validators.required,
      Validators.maxLength(200),
    ]),
    FacilityRelatedItemID: new FormControl('', [
      Validators.required,
      Validators.maxLength(20),
    ]),
    Purpose: new FormControl('', [
      Validators.required,
      Validators.maxLength(20),
    ]),
    PermitinitialQty: new FormControl(0, [
      Validators.required,
      Validators.maxLength(200),
    ]),
    ItemCategory: new FormControl(0, [
      Validators.required,
      Validators.maxLength(200),
    ]),
    Model: new FormControl('', [
      Validators.required,
      Validators.maxLength(200),
    ]),
    ItemTypeNumber: new FormControl('', [Validators.maxLength(200)]),
    ItemTypeName: new FormControl('', [Validators.maxLength(200)]),
    HSCode: new FormControl('', [Validators.required]),
    EndUserCertificate: new FormControl(false, []),
    GovernmentCommitments: new FormControl(false, []),
    NuclearEntityId: new FormControl(0, [Validators.required]),
    Manufacturer: new FormControl(27, [Validators.required]),
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
    private relateditemsService: RelatedItemsService,
    private titleService: Title,
    private clipboardApi: ClipboardService,
    private ManufacturerSrvc: ManufacturerService
  ) {}

  ngOnInit(): void {
    this.getData();

    this.commonServices
      .getLookupByClassName('NuclearEntity')
      .subscribe((res) => {
        this.NuclearEntity = res['data']['lookupSetTerms'];
      });

    this.commonServices
      .getLookupByClassName('ItemCategory')
      .subscribe((res) => {
        console.log(res['data']['lookupSetTerms']);
        this.ItemCategory = res['data']['lookupSetTerms'];
      });

    this.ManufacturerSrvc.GetAll().subscribe((res) => {
      this.Manufacturer = res['data'];
    });
  }

  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe((value) => {
      this.translateService
        .stream('RelatedItems.PageTitle')
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

  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }

  Back() {
    this.router.navigate(['/']);
  }

  addSave() {
    return this.relateditemsService.Add(this.DataObject);
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

  ExportExel(): void {
    /* pass here the table id */
    let element = document.getElementById('relatedItemsDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, 'relatedItemsDataTable.xlsx');
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('General.SuccessExportExcel'),
      detail: this.translateService.instant('General.SuccessExportExcel'),
      life: 6000,
    });
  }
  Print() {
    let element = document.getElementById('relatedItemsDataTable');
    let newWin = window.open('');
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('relatedItemsDataTable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('General.SuccessCopy'),
      detail: this.translateService.instant('General.SuccessCopy'),
      life: 6000,
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

  getCountries() {
    var _this = this;
    _this.commonServices.getCountriesClear().subscribe(function (data) {
      var response = data as Response<BasCoutriesText[]>;
      if (response.succeeded) {
        _this.CountryList = response.data;
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

  update(id: string): void {
    this.router.navigate(['related-items-setup'], {
      queryParams: { code: id },
    });
  }

  Details(obj: any) {
    var _this = this;
    _this.DataObject = <DTORelatedItems>{};
    console.log(obj.relatedItemCode);
    //_this.SubmitForm.controls['UPDRelatedItemCode'].setValue(obj.relatedItemCode);
    //console.log("Gayan - Details");
    // _this.DataObject.RelatedItemCode = obj.relatedItemCode;
    // _this.DataObject.Description = obj.description;
    // _this.DataObject.ManufacturerSerialNom = obj.manufacturerSerialNom;
    // _this.DataObject.FacilityRelatedItemID = obj.facilityRelatedItemID;
    // _this.DataObject.Purpose = obj.purpose;
    // _this.DataObject.PermitinitialQty = obj.permitinitialQty;
    // _this.DataObject.ItemCategory = obj.itemCategory;
    // _this.DataObject.Model = obj.model;
    // _this.DataObject.ItemTypeNumber = obj.itemTypeNumber;
    // _this.DataObject.ItemTypeName = obj.itemTypeName;
    // _this.DataObject.HSCode = obj.hsCode;
    // _this.DataObject.StatusID = 6;
    // _this.DataObject.EndUserCertificate = obj.endUserCertificate;
    // _this.DataObject.GovernmentCommitments = obj.governmentCommitments;
    // _this.DataObject.NuclearEntityId = obj.nuclearEntityId;
    // _this.DataObject.Manufacturer = obj.manufacturer;
    // _this.Update = 1;
    // this.router.navigate(['related-items-setup'], {
    //   state: { DataObject: this.DataObject },
    // });
  }

  view(id: string) {
    this.router.navigate(['related-items-setup'], {
      queryParams: { code: id, isView: true },
    });
  }

  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message:
        _this.translateService.instant('Confirmation.deleteMsg') +
        ': ' +
        obj.manufacturerSerialNom +
        ' --> ' +
        obj.description,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        _this.relateditemsService
          .DeleteById(obj.relatedItemCode)
          .subscribe(function (data) {
            var response = data as Response<boolean>;
            if (response.succeeded) {
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

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
      dtInstance.ajax.reload();
    });
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
        this.relateditemsService
          .GetAllFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              console.log('resp: ', resp);
              this.dtTrigger.next();
              this.DataList = resp['data'];
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
        { data: 'manufacturerSerialNom' },
        { data: 'hierarchyStructureCode' },
        { data: 'isTechnologyAndSoftware' },
        { data: 'description' },
        { data: null },
      ],
    };
  }
}
