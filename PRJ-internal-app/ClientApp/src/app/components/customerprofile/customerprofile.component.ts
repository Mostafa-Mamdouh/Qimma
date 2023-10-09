import { Component, OnInit, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
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
import { CustomerProfileService } from '../../../services/CustomerProfile/CustomerProfile.service';
import { DTOCustomerProfile } from '../../../models/CustomerProfile/customerProfileModel';
import BasCitesText from '../../../models/Common/BasCites.Text';
import { DataTableDirective } from 'angular-datatables';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard'; 

@Component({
  selector: 'app-customerprofile',
  templateUrl: './customerprofile.component.html',
  styleUrls: ['./customerprofile.component.css']
})
export class CustomerprofileComponent implements OnInit {

  dtOptions: any = {};
  @ViewChild(DataTableDirective)
  dtElement!: DataTableDirective;
  dtTrigger: any = new Subject();
  private _searchCriteria: any = { isPageLoad: true, filter: '' };

  currentUser: UserData = <UserData>{};
  DataObject: DTOCustomerProfile = <DTOCustomerProfile>{};
  DataList: any[] = [];

  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;

  dtInstanceObj: any;
  initialized: boolean = false;

  SubmitForm = new FormGroup({
    CustomerId: new FormControl(0), 
    ActiveFlag: new FormControl(false), 
    CustomerNameAr: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    CustomerNameEn: new FormControl('', [Validators.required, Validators.maxLength(200)]),
    RefCode: new FormControl('', [Validators.maxLength(50)]),
  });


  constructor(@Inject(forwardRef(() => AppComponent)) private app: AppComponent,
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
  private CustomerProfileService: CustomerProfileService,
  private titleService: Title,
  private clipboardApi: ClipboardService) {
    this.route.queryParams.subscribe(params => {
    });
  }

  ngOnInit(): void {
    this.getData();
  }

  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('CustomerProfile.PageTitle').subscribe(valueTitle => {
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
    this.router.navigate(['system-settings']);
  }

  addSave() {
    return this.CustomerProfileService.Add(this.DataObject);
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

  reset() {
    this.Update = 0;
    this.SubmitForm.reset();
  }

  save() {
    var _this = this;
    
    if (_this.Update == 0) {
      this.SubmitForm.controls['CustomerId'].setValue(0);
    }

    let body = <DTOCustomerProfile>this.SubmitForm.value;

    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      if (_this.SubmitForm.valid) {
         console.log(JSON.stringify(this.SubmitForm.value));
        _this.CustomerProfileService.Add(body).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded) {
            _this.reload();
            _this.SubmitForm.reset();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
          } else {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors?.join('=>'), life: 6000 });
          }
        })
      }
      else {
        this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.RequiredAllFields'), detail: _this.translateService.instant('General.RequiredAllFields'), life: 6000 });
      }
    }
    else {

      var _this = this;
      //this.SubmitForm.controls['RelatedItemCode'].setValue(this.SubmitForm.controls['UPDRelatedItemCode'].getValue());
      let body = <DTOCustomerProfile>this.SubmitForm.value;

       //console.log(JSON.stringify(this.SubmitForm.value));
      _this.CustomerProfileService.Update(body).subscribe(function (data) {
        var response = data as Response<boolean>;
        if (response.succeeded) {
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
    let element = document.getElementById('customerprofileDataTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    /* save to file */
    XLSX.writeFile(wb, "customerprofileDataTable.xlsx");
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessExportExcel'), detail: this.translateService.instant('General.SuccessExportExcel'), life: 6000 });

  }
  Print() {
    let element = document.getElementById('customerprofileDataTable');
    let newWin = window.open("");
    newWin.document.write(element.outerHTML);
    newWin.print();
    newWin.close();
  }
  Copy() {
    let element = document.getElementById('customerprofileDataTable');
    this.clipboardApi.copyFromContent(element.textContent);
    this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: this.translateService.instant('General.SuccessCopy'), detail: this.translateService.instant('General.SuccessCopy'), life: 6000 });
  }
  reload() {
    var _this = this;
    _this.getData();
    _this.rerender();
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
        this.CustomerProfileService
          .GetAllFunctional(dataTablesParameters)
          .subscribe((resp) => {
            if (resp['succeeded']) {
              //console.log('resp: ', resp);
              this.dtTrigger.next();
              this.DataList = resp['data'];

              console.log(this.DataList)
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
        { data: 'customerNameAr' },
        { data: 'customerNameEn' },
        { data: 'refCode' },
        { data: 'activeFlag' },
        { data: null },
      ],
    };
  }


  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.dtTrigger.next();
      dtInstance.ajax.reload();
    });
  }

  Details(obj: any) {
    var _this = this;
    _this.DataObject = <DTOCustomerProfile>{};
    console.log('Update-->'+obj);
    //_this.SubmitForm.controls['UPDRelatedItemCode'].setValue(obj.relatedItemCode);
    //console.log(_this.DataObject);
    _this.DataObject.CustomerId = obj.customerId;
    _this.DataObject.CustomerNameAr = obj.customerNameAr;
    _this.DataObject.CustomerNameEn = obj.customerNameEn;
    _this.DataObject.RefCode = obj.refCode;
    _this.DataObject.ActiveFlag = obj.activeFlag;
    _this.Update = 1; 
  }

  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.customerNameEn + " --> " + obj.refCode,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        console.log(obj.customerId);
        _this.CustomerProfileService.DeleteById(obj.customerId).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded ) {
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

}
