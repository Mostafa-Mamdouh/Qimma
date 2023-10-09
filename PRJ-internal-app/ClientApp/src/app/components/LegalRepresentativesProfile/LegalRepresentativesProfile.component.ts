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
import { Subscription } from 'rxjs';
import { SharedService } from '../../services/shared.service';
import { CommonClass } from '../../common/CommonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from "@angular/platform-browser";
import { CommonServices } from '../../../services/Common/common.service';
import { DTOLegalRepProfile } from '../../../models/LegalRepresentativesProfile/LegalRepProfile';
import { LegalRepProfileServices } from '../../../services/LegalRepProfile/LegalRepProfile.service';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';


declare var $: any;

@Component({
  selector: 'LegalRepresentativesProfile',
  templateUrl: './LegalRepresentativesProfile.component.html',
  styleUrls: ['./LegalRepresentativesProfile.component.css']
})

export class LegalRepresentativesProfileComponent implements OnInit, AfterViewInit {

  currentUser: UserData = <UserData>{};
  DataObject: DTOLegalRepProfile = <DTOLegalRepProfile>{};
  DataList: any;
  CountryList: any;
  CityList: any;

  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;


  SubmitForm = new FormGroup({
    LegalRepresentativesNameAr : new FormControl (''),
    LegalRepresentativesNameEn : new FormControl(''),
    Title: new FormControl(''),
    NationalID: new FormControl(''),
    PhoneNo: new FormControl(''),
    MobileNo: new FormControl(''),
    EmailId: new FormControl(''),
    Status: new FormControl(''),
    AmanInsertedOn: new FormControl(''),
    AmanID: new FormControl(''),
    CurrentFacilities: new FormControl(''),
    SourceType: new FormControl(''),
    Note: new FormControl(''),
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
    private LegalRepProfileServices: LegalRepProfileServices,
    private titleService: Title,
    private clipboardApi: ClipboardService  ) {
    this.route.queryParams.subscribe(params => {

    });
  }

  ngOnInit(): void {
    this.getUserData();

    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('BasCountries.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
  }
  getUserData(): void {
    var _this = this;
    _this.currentUser=_this.userService.getCurrentUser();
    _this.getData();
  }
  getData() {
    var _this = this;
    _this.LegalRepProfileServices.GetAll().subscribe(function (data) {
      var response = data as Response<DTOLegalRepProfile[]>;
      if (response.succeeded) {
        _this.DataList = response.data;
        _this.getCountries();

        setTimeout(() => {
          $('#lookUpsDataTable').DataTable({
            pagingType: 'full_numbers',
            pageLength: 10,
            processing: false,
            lengthMenu: [5, 10, 25]
          });
        }, 1);
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
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

        _this.LegalRepProfileServices.Delete(obj.id).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.getData();
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
  reset() {
    this.Update = 0;
    this.SubmitForm.reset();
  }
  Details(obj: any) {
    var _this = this;
    _this.DataObject = <DTOLegalRepProfile>{};
    _this.DataObject.id = obj.id;
    _this.DataObject.AmanID = obj.AmanID;
    _this.DataObject.AmanInsertedOn = obj.amanInsertedOn;
    _this.DataObject.LegalRepresentativesNameAr = obj.legalRepresentativesNameAr;
    _this.DataObject.LegalRepresentativesNameEn = obj.legalRepresentativesNameEn
    _this.DataObject.MobileNo = obj.mobileNo
    _this.DataObject.PhoneNo = obj.phoneNo
    _this.DataObject.SourceType = obj.sourceType
    _this.DataObject.Title = obj.title
    _this.DataObject.Status = obj.status
    _this.DataObject.EmailId = obj.emailId
    _this.DataObject.Note = obj.note
    _this.DataObject.CurrentFacilities = obj.currentFacilities
    _this.Update = 1;
  }
  save() {
    var _this = this;

    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      if (_this.SubmitForm.valid) {
        _this.LegalRepProfileServices.Add(JSON.stringify(this.SubmitForm.value)).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.getData();
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


      _this.LegalRepProfileServices.Update(JSON.stringify(this.SubmitForm.value)).subscribe(function (data) {
        var response = data as Response<boolean>;
        if (response.succeeded && response.data) {
          _this.getData();
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
    XLSX.writeFile(wb, "LegalSheet.xlsx");
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
}
