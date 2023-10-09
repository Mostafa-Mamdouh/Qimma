import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AppComponent } from '../../../app.component';
import UserData from '../../../../models/Common/userdata';
import { Response } from '../../../../models/Common/response';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { Title } from "@angular/platform-browser";
import { CommonServices } from '../../../../services/Common/common.service';
import { LookupSet } from '../../../../models/LookupSet/LookupSet';
import { UserInfoServices } from '../../../../services/userinfo.service';
import { FieldObjectEmmiter, FieldTypeList, ScreenField } from '../../../../models/ScreenManagement/ScreenField';
import { FieldType } from '../../../../Enumerations/Enums';
import { ScreenFieldService } from '../../../../services/Screen/screen-field.service';
import { LovService } from '../../../../services/Lov/lov.service';
import { Lov } from '../../../../models/LovManagement/Lov';

declare var $: any;

@Component({
  selector: 'screen-field',
  templateUrl: './screen-field.component.html',
  styleUrls: ['./screen-field.component.css']
})

export class ScreenFieldComponent implements OnInit, AfterViewInit {
  currentUser: UserData = <UserData>{};
  objData: ScreenField = <ScreenField>{};
  dataList: any;
  screenFieldObj: any;
  ParamId: string = "";
  classList: LookupSet[];
  lovList: Lov[];
  @Input() isPreviewOnly = false;
  @Input() selectedFields: ScreenField[];
  @Output() clickedField = new EventEmitter<FieldObjectEmmiter>();




  FieldTypeList: FieldTypeList[] = [{ FieldType: +FieldType.Date, FieldTypeNameEn: 'Date', FieldTypeNameAr: 'تاريخ' }
    , { FieldType: +FieldType.ListItem, FieldTypeNameEn: 'ListItem', FieldTypeNameAr: 'قائمة منسدلة' }
    , { FieldType: +FieldType.Text, FieldTypeNameEn: 'Text', FieldTypeNameAr: 'حقل' }
    , { FieldType: +FieldType.TextArea, FieldTypeNameEn: 'TextArea', FieldTypeNameAr: ' حقل كبير' }

    , { FieldType: +FieldType.RadioButton, FieldTypeNameEn: 'RadioButon', FieldTypeNameAr: 'زر الراديو' }
    , { FieldType: +FieldType.CheckBox, FieldTypeNameEn: 'CheckBox', FieldTypeNameAr: 'خانة الاختيار' }
    , { FieldType: +FieldType.Button, FieldTypeNameEn: 'Button', FieldTypeNameAr: 'زر' }
    , { FieldType: +FieldType.FileUpload, FieldTypeNameEn: 'FileUpload', FieldTypeNameAr: 'تحميل الملف' }

  ];
  FieldDescAr: string = null;
  FieldDescEn: string = null;
  FieldType: FieldType = FieldType.Date;
  FieldFormat: string = null;
  FieldFormatHidden: boolean = false;

  LookupSetId: number = null;
  LovId: number = null;
  LookupSetHidden: boolean = false;
  LovHidden: boolean = false;
  fieldId: number;

  ScreenId: number;

  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;


  SubmitForm = new FormGroup({
    FieldDescAr: new FormControl(null, [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    FieldDescEn: new FormControl(null, [Validators.required, Validators.maxLength(200), Validators.minLength(3)]),
    FieldType: new FormControl(null, [Validators.required]),
    FieldFormat: new FormControl(null, [Validators.maxLength(20)]),
    LookupSetId: new FormControl([null, Validators.required]),
    LovId: new FormControl([null, Validators.required]),
    ScreenId: new FormControl([null, Validators.required]),
  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private commonServices: CommonServices,
    private screenFieldServices: ScreenFieldService,
    private lovServices: LovService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private titleService: Title) {
    this.route.queryParams.subscribe(params => {
      this.ParamId = params["Id"];
    });
  }

  ngOnChanges() {
    if (this.isPreviewOnly == true) {
      this.getSelectedData();
    }
  }

  ngOnInit(): void {
    if (this.isPreviewOnly == false) {
      this.getUserData();
      this.getClassList();
      this.getLovList();
    }
    
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('ScreenFields.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
  }
  getClassList() {
    var _this = this;
    _this.commonServices.getLookups().subscribe(function (data) {
      var response = data as Response<LookupSet[]>;
      if (response.succeeded) {
        _this.classList = response.data;
        console.log(_this.classList)
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }
  getLovList() {
    var _this = this;
    _this.lovServices.GetAllLov().subscribe(function (data) {
      var response = data as Response<Lov[]>;
      if (response.succeeded) {
        _this.lovList = response.data;
        console.log(_this.lovList)
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }

  getUserData(): void {
    var _this = this;

    _this.currentUser=_this.userService.getCurrentUser();
    _this.getData();
  }
  getData() {
    var _this = this;
    _this.screenFieldServices.GetByScreenFieldsId(_this.ParamId).subscribe(function (data) {
      var response = data as Response<ScreenField[]>;
      if (response.succeeded) {
        _this.dataList = response.data;
        console.log(_this.dataList)
        $('#datatableexample').DataTable().destroy();
        setTimeout(() => {
          
          $('#datatableexample').DataTable({
            pagingType: 'full_numbers',
            pageLength: 5,
            processing: true,
            lengthMenu: [5, 10, 25]
          });
        }, 1);
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }
  getSelectedData() {
    var _this = this;
    _this.dataList = this.selectedFields;
    $('#datatableexample').DataTable().destroy();
    setTimeout(() => {
      $('#datatableexample').DataTable({
        pagingType: 'full_numbers',
        pageLength: 4,
        processing: true,
        lengthMenu: [4],
        bLengthChange: false
      });
    }, 1);
  }

  Delete(obj: any) {
    var _this = this;
    _this.reset();
    _this.app.confirmationService.confirm({
      message: _this.translateService.instant('Confirmation.deleteMsg') + ": " + obj.className,
      header: _this.translateService.instant('Confirmation.confirm'),
      acceptLabel: _this.translateService.instant('Confirmation.Yes'),
      rejectLabel: _this.translateService.instant('Confirmation.No'),
      icon: 'pi pi-exclamation-triangle',
      accept: () => {

        _this.commonServices.deleteLookUpTerm(obj.id).subscribe(function (data) {
          var response = data as Response<boolean>;
          if (response.succeeded && response.data) {
            _this.getData();
            _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.DeletedSuccessfully'), detail: _this.translateService.instant('General.DeletedSuccessfully'), life: 6000 });
          }
          else if (response.succeeded == false && response.message == "NOTEXIST") {
            _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.NotFound'), detail: this.translateService.instant('General.NotFound'), life: 6000 });
          } else {
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
    console.log(obj)

    this.fieldId = obj.id;

    this.FieldDescAr = obj.fieldDescAr;
    this.FieldDescEn = obj.fieldDescEn;
    this.FieldFormat = obj.fieldFormat;
    this.FieldType = obj.fieldType;
    this.ScreenId = obj.screenId;
    this.onChangeType();
    this.LookupSetId = obj.lookupSetId;
    this.LovId = obj.lovId;

    this.Update = 1;
  }
  save() {
    var _this = this;

    var lang = _this.translateService.currentLang;
    if (_this.Update == 0) {
      if (_this.SubmitForm.valid) {
        var objSave = {
          "ScreenIdValue": this.ParamId,
          "LookupSetId": _this.LookupSetId,
          "LovId": _this.LovId,
          "FieldDescAr": _this.FieldDescAr,
          "FieldDescEn": _this.FieldDescEn,
          "FieldFormat": _this.FieldFormat,
          "FieldType": _this.FieldType,
        };
        _this.screenFieldServices.AddScreenFields(objSave).subscribe(function (data) {
          var response = data as Response<any>;
          if (response.succeeded && response.data) {
            _this.dataList.push(response.data)
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

      var objUpdate = {
        "Id": _this.fieldId,
        "ScreenIdValue": _this.ParamId,
        "LookupSetId": _this.LookupSetId,
        "LovId": _this.LovId,
        "FieldDescAr": _this.FieldDescAr,
        "FieldDescEn": _this.FieldDescEn,
        "FieldFormat": _this.FieldFormat,
        "FieldType": _this.FieldType,
      };
      var _this = this;
      _this.screenFieldServices.UpdateScreenFields(objUpdate).subscribe(function (data) {
        var response = data as Response<any>;
        if (response.succeeded && response.data) {
          _this.dataList.push(response.data)
          _this.reset();
          _this.getData();

          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else if (response.succeeded == false && response.message == "NOTEXIST") {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.NotFound'), detail: this.translateService.instant('General.NotFound'), life: 6000 });
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }
      })
    }
  }
  Back() {
    this.router.navigate(['screens']);
  }

  onChangeType() {
    if (this.FieldType == FieldType.Date) {
      this.LovId = null;
      this.LookupSetId = null;
      this.LookupSetHidden = true;
      this.LovHidden = true;
      this.FieldFormatHidden = false;
      this.SubmitForm.get('LovId').setValidators(null);
      this.SubmitForm.get('LovId').updateValueAndValidity();
      this.SubmitForm.get('LookupSetId').setValidators(null);
      this.SubmitForm.get('LookupSetId').updateValueAndValidity();
      this.SubmitForm.get('FieldFormat').setValidators(Validators.required);
      this.SubmitForm.get('FieldFormat').updateValueAndValidity();
    }
    else if (this.FieldType == FieldType.Text || this.FieldType == FieldType.TextArea) {
      this.LovId = null;
      this.LookupSetId = null;
      this.FieldFormat = null;
      this.LookupSetHidden = true;
      this.LovHidden = true;

      this.FieldFormatHidden = true;
      this.SubmitForm.get('LovId').setValidators(null);
      this.SubmitForm.get('LovId').updateValueAndValidity();
      this.SubmitForm.get('LookupSetId').setValidators(null);
      this.SubmitForm.get('LookupSetId').updateValueAndValidity();
      this.SubmitForm.get('FieldFormat').setValidators(null);
      this.SubmitForm.get('FieldFormat').updateValueAndValidity();
    }
    else if (this.FieldType == FieldType.ListItem) {
      this.LovId = null
      this.FieldFormat = null;
      this.LovHidden = false;
      this.LookupSetHidden = false;
      this.FieldFormatHidden = true;
      this.FieldFormat = null;
      this.SubmitForm.get('FieldFormat').setValidators(null);
      this.SubmitForm.get('FieldFormat').updateValueAndValidity();
      this.SubmitForm.get('LookupSetId').setValidators(Validators.required);
      this.SubmitForm.get('LookupSetId').updateValueAndValidity();
      this.SubmitForm.get('LovId').setValidators(Validators.required);
      this.SubmitForm.get('LovId').updateValueAndValidity();
    }

  }

  onSelectFieldList(controlName: string) {
    if (controlName == 'LookupSetId') {
      if (this.LookupSetId) {
        this.LovId = null
        this.LovHidden = true;
        this.SubmitForm.get('LookupSetId').setValidators(Validators.required);
        this.SubmitForm.get('LookupSetId').updateValueAndValidity();
        this.SubmitForm.get('LovId').setValidators(null);
        this.SubmitForm.get('LovId').updateValueAndValidity();
      }
      else {
        this.LovHidden = false;
        this.SubmitForm.get('LovId').setValidators(Validators.required);
        this.SubmitForm.get('LovId').updateValueAndValidity();
      }
    }
    else if (controlName == 'LovId') {
      if (this.LovId) {
        this.LookupSetId = null
        this.LookupSetHidden = true;
        this.SubmitForm.get('LookupSetId').setValidators(null);
        this.SubmitForm.get('LookupSetId').updateValueAndValidity();
        this.SubmitForm.get('LovId').setValidators(Validators.required);
        this.SubmitForm.get('LovId').updateValueAndValidity();
      }
      else {
        this.LookupSetHidden = false;
        this.SubmitForm.get('LookupSetId').setValidators(Validators.required);
        this.SubmitForm.get('LookupSetId').updateValueAndValidity();
      }
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

  fieldClicked(evt: any) {
    console.log("dddd")

    console.log(evt)
    if (evt.fieldType == FieldType.ListItem && (evt.lookupSetId!='' || evt.lovId!='')) {
      var fieldEmitter: FieldObjectEmmiter = new FieldObjectEmmiter();
      fieldEmitter.fieldId = evt.fieldId;
      if (evt.lookupSetId !='') {
        fieldEmitter.isLov = false;
        fieldEmitter.lookupId = evt.lookupSetId;
      }
      else {
        fieldEmitter.isLov = true;
        fieldEmitter.lookupId = evt.lovId;
      }
      this.clickedField.emit(fieldEmitter)
    }
  }

}
