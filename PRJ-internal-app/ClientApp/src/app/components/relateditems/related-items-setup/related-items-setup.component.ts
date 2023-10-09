import {
  Component,
  forwardRef,
  Inject,
  ViewChild,
  ChangeDetectorRef,
  AfterViewInit,
  Input,
} from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Navigation, Router } from '@angular/router';
import {
  faTrash,
  faPenToSquare,
  faArrowAltCircleLeft,
} from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { AppComponent } from '../../../app.component';
import { UserInfoServices } from '../../../../services/userinfo.service';
import UserData from '../../../../models/Common/userdata';
import { Response } from '../../../../models/Common/response';
import BasCountries from '../../../../models/Common/BasCoutries';
import BasCoutriesText from '../../../../models/Common/BasCoutries.Text';
import { FormControl } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Title } from '@angular/platform-browser';
import { CommonServices } from '../../../../services/Common/common.service';
import {
  DTORelatedItems,
  RelatedItemFiles,
} from '../../../../models/RelatedItems/relateditemsModel';
import BasCitesText from '../../../../models/Common/BasCites.Text';
import { DataTableDirective } from 'angular-datatables';
import * as XLSX from 'xlsx';
import { ClipboardService } from 'ngx-clipboard';
import { ManufacturerService } from 'src/services/ManufacturerMaster/Manufacturer.service';
import { RelatedItemsService } from '../../../../services/RelatedItems/relatedItems.service';
import { AttachmentsTypes } from 'src/Enumerations/Enums';
import { Base64ToPdf, PdfToBase64 } from 'src/helper/fileToBase64';
@Component({
  selector: 'app-related-items-setup',
  templateUrl: './related-items-setup.component.html',
  styleUrls: ['./related-items-setup.component.css'],
})
export class RelatedItemsSetupComponent implements OnInit {
  updateId: any;

  currentUser: UserData = <UserData>{};
  DataObject: any = null;
  DataList: any[] = [];
  CountryList: any;
  CityList: any;
  hierarchyItems: string[] = [];

  lookups: any[] = [];

  NuclearEntity?: any = [];
  ItemCategory?: any = [];
  Manufacturer?: any = [];

  faArrowAltCircleLeft = faArrowAltCircleLeft;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;

  // Attachments
  AttachmentsTypes = AttachmentsTypes;
  sourceFiles: File[] = [];
  isSourceAttachmentValid: boolean = true;

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  customExportPermits: File[] = [];
  shipperExportPermits: File[] = [];
  otherDocumnets: File[] = [];
  charCertificates: File[] = [];
  sourceTagImages: File[] = [];
  endUserCertificate: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;
  isImagesValid: boolean = true;
  isCharCertificateValid: boolean = true;
  isSourceTagImagesValid: boolean = true;

  isManufacturerCertificate: boolean = true;
  isCharCertificate: boolean = false;
  isCustomImportPermit: boolean = true;
  isShipperImportPermit: boolean = true;
  isImage: boolean = true;
  isSourceTagImage = false;

  //view
  isView: boolean = false;

  // Form Validation Variables
  submitBtnClicked: boolean = false;

  ItemForm = this.fb.group({
    nuclearRelatedItem: ['', Validators.required],
    isTechnologyAndSoftware: [false],
    description: [
      '',
      [Validators.required, Validators.minLength(5), Validators.maxLength(200)],
    ],
    status: ['', Validators.required],
    manufacturerSerialNo: ['', Validators.required],
    manufacturerId: ['', Validators.required],
    manufacturerCountryId: ['', Validators.required],
    relatedItemFiles: this.fb.array([]),
    noManufacturerCertificateFlag: [false, Validators.required],
    noCustomImportPermitFlag: [false, Validators.required],
    noShipperImportPermitFlag: [false, Validators.required],
    noImagesFlag: [false, Validators.required],
    noCharacterizationCertificateFlag: [false, Validators.required],
    noSourceTagImageFlag: [false, Validators.required],
  });
  updateObj: any;

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private commonServices: CommonServices,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    private ConfirmationService: ConfirmationService,
    private MessageService: MessageService,
    private commonClass: CommonClass,
    private relateditemsService: RelatedItemsService,
    private titleService: Title,
    private clipboardApi: ClipboardService,
    private fb: FormBuilder
  ) {
    this.route.queryParams.subscribe((params) => {
      this.updateId = params['code'];
      this.isView = params['isView'];
    });
    let nav: Navigation = this.router.getCurrentNavigation();

    if (nav.extras && nav.extras.state && nav.extras.state['DataObject']) {
      this.DataObject = nav.extras.state['DataObject'];
      console.log('this.DataObject: ', this.DataObject);
    }
  }

  ngOnInit(): void {
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    this.commonServices.getRelatedItemsSetupLookup().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
        console.log(this.lookups);
      });
      if (this.updateId) {
        this.relateditemsService.GetById(this.updateId).subscribe((res) => {
          console.log(res);
          this.updateObj = res['data'];
          this.setUpdateData();
        });
      }
    });

    this.relateditemsService.GetAsLookup().subscribe((res) => {
      this.hierarchyItems = res['data'];
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
  }

  Breadcrumb(rValue: string) {
    this.router.navigate([rValue]);
  }

  Back() {
    this.router.navigate(['related-items']);
  }

  // Attachments Methods
  get sourceAttachments() {
    return this.ItemForm.get('relatedItemFiles') as FormArray;
  }

  addSourceAttachments(file, attachmentType) {
    let StrBase64;

    PdfToBase64(file)
      .then((res) => (StrBase64 = res))
      .then((res) => {
        return {
          fileSourceID: '',
          name: file.name,
          size: file.size,
          base64: StrBase64.toString(),
          contentType: file.type,
          attachmentType: attachmentType,
        };
      })
      .then((res) => {
        this.sourceAttachments.push(this.fb.group(res));
      });
  }

  removeSourceAttachment(i: number) {
    this.sourceAttachments.removeAt(i);
  }

  onSelect(event, field) {
    console.log(event.addedFiles);
    this.sourceFiles = [];
    this.sourceFiles.push(...event.addedFiles);
    for (let i = 0; i < this.sourceFiles.length; i++) {
      this.addSourceAttachments(<File>this.sourceFiles[i], field);

      if (field == AttachmentsTypes.manufacturerCertificate)
        this.manufacturerCertificates.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.customImportPermits)
        this.customImportPermits.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.shipperImportPermits)
        this.shipperImportPermits.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.charCertificates)
        this.charCertificates.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.sourceTagImage)
        this.sourceTagImages.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.endUserCertificate)
        this.endUserCertificate.push(this.sourceFiles[i]);
      else this.otherDocumnets.push(this.sourceFiles[i]);
    }
  }

  onRemove(event, field) {
    this.sourceFiles = [];

    if (field == AttachmentsTypes.manufacturerCertificate) {
      this.manufacturerCertificates.splice(
        this.manufacturerCertificates.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    } else if (field == AttachmentsTypes.customImportPermits) {
      this.customImportPermits.splice(
        this.customImportPermits.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    } else if (field == AttachmentsTypes.shipperImportPermits) {
      this.shipperImportPermits.splice(
        this.shipperImportPermits.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    } else if (field == AttachmentsTypes.charCertificates) {
      this.charCertificates.splice(this.charCertificates.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    } else if (field == AttachmentsTypes.sourceTagImage) {
      this.sourceTagImages.splice(this.sourceTagImages.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    } else if (field == AttachmentsTypes.endUserCertificate) {
      this.endUserCertificate.splice(this.endUserCertificate.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    } else {
      this.otherDocumnets.splice(this.otherDocumnets.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
      this.sourceFiles.push(...this.endUserCertificate);
    }

    this.ItemForm.controls['relatedItemFiles'].clear();

    for (let i = 0; i < this.manufacturerCertificates.length; i++) {
      this.addSourceAttachments(
        <File>this.manufacturerCertificates[i],
        AttachmentsTypes.manufacturerCertificate
      );
    }
    for (let i = 0; i < this.customImportPermits.length; i++) {
      this.addSourceAttachments(
        <File>this.customImportPermits[i],
        AttachmentsTypes.customImportPermits
      );
    }

    for (let i = 0; i < this.shipperImportPermits.length; i++) {
      this.addSourceAttachments(
        <File>this.shipperImportPermits[i],
        AttachmentsTypes.shipperImportPermits
      );
    }
    for (let i = 0; i < this.otherDocumnets.length; i++) {
      this.addSourceAttachments(
        <File>this.otherDocumnets[i],
        AttachmentsTypes.otherDocumnets
      );
    }

    for (let i = 0; i < this.sourceTagImages.length; i++) {
      this.addSourceAttachments(
        <File>this.sourceTagImages[i],
        AttachmentsTypes.sourceTagImage
      );
    }

    for (let i = 0; i < this.charCertificates.length; i++) {
      this.addSourceAttachments(
        <File>this.charCertificates[i],
        AttachmentsTypes.charCertificates
      );
    }

    for (let i = 0; i < this.endUserCertificate.length; i++) {
      this.addSourceAttachments(
        <File>this.endUserCertificate[i],
        AttachmentsTypes.endUserCertificate
      );
    }
  }

  validateAttachments(): boolean {
    let list = this.ItemForm.controls['relatedItemFiles'].value;
    if (this.isManufacturerCertificate)
      this.isSourceManufacturerCertificateValid =
        this.manufacturerCertificates.length == 0 ? false : true;
    else this.isSourceManufacturerCertificateValid = true;

    if (this.isCharCertificate)
      this.isCharCertificateValid =
        this.charCertificates.length == 0 ? false : true;
    else this.isCharCertificateValid = true;

    if (this.isCustomImportPermit) {
      this.isSourceCustomImportPermitsValid =
        this.customImportPermits.length == 0 ? false : true;
    } else this.isSourceCustomImportPermitsValid = true;

    if (this.isShipperImportPermit) {
      this.isSourceShipperImportPermitsValid =
        this.shipperImportPermits.length == 0 ? false : true;
    } else this.isSourceShipperImportPermitsValid = true;

    if (this.isSourceTagImage) {
      this.isSourceTagImagesValid =
        this.sourceTagImages.length == 0 ? false : true;
    } else this.isSourceTagImagesValid = true;
    if (
      this.isSourceManufacturerCertificateValid &&
      this.isCharCertificateValid &&
      this.isSourceCustomImportPermitsValid &&
      this.isSourceShipperImportPermitsValid &&
      this.isImagesValid &&
      this.isSourceTagImagesValid
    ) {
      this.ItemForm.controls['relatedItemFiles'].setErrors(null);
      this.isSourceAttachmentValid = true;
      return true;
    }

    return false;
  }

  handleCertificateChange(choice) {
    let noManufacturerCertificate =
      this.ItemForm.controls['noManufacturerCertificateFlag'];
    let noCharCertificate =
      this.ItemForm.controls['noCharacterizationCertificateFlag'];
    let noSourceTag = this.ItemForm.controls['noSourceTagImageFlag'];
    let noCustomImport = this.ItemForm.controls['noCustomImportPermitFlag'];
    let noShipperImportPermit =
      this.ItemForm.controls['noShipperImportPermitFlag'];
    let imagesFlag = this.ItemForm.controls['noImagesFlag'];

    if (choice == 1) {
      this.isManufacturerCertificate = !noManufacturerCertificate.value;
      this.isCharCertificate = !this.isManufacturerCertificate;
      this.isSourceTagImage = false;
      noCharCertificate.setValue(this.isManufacturerCertificate);
      noSourceTag.setValue(this.isManufacturerCertificate);
    } else if (choice == 2) {
      this.isCharCertificate = !noCharCertificate.value;
      this.isSourceTagImage = !this.isCharCertificate;
      this.isManufacturerCertificate = false;
      noManufacturerCertificate.setValue(true);
      noSourceTag.setValue(this.isCharCertificate);
    } else if (choice == 3) {
      this.isCustomImportPermit = !noCustomImport.value;
    } else if (choice == 4) {
      this.isShipperImportPermit = !noShipperImportPermit.value;
    } else if (choice == 5) {
      this.isImage = !imagesFlag.value;
    } else if (choice == 6) {
      this.isSourceTagImage = !noSourceTag.value;
    }
  }

  setUpdateData() {
    let manufacturer = this.updateObj.manufacturerId
      ? this.lookups['Manufacturer'].find(
          (x) => x.id == this.updateObj.manufacturerId
        )
      : null;

    let status = this.updateObj.statusID
      ? this.lookups['Status'].find((x) => x.id == this.updateObj.statusID)
      : null;

    let mfCountry = this.updateObj.manufacturerCountryId
      ? this.lookups['ManufacturerCountry'].find(
          (x) => x.id == this.updateObj.manufacturerCountryId
        )
      : null;

    let hierarchyCode = this.updateObj.hierarchyStructureCode
      ? this.hierarchyItems.find(
          (x) => x['itemStructureCode'] == this.updateObj.hierarchyStructureCode
        )
      : null;

    if (this.updateObj.relatedItemFiles.length > 0) {
      this.updateObj.relatedItemFiles?.map((attachment) => {
        console.log(attachment);
        let file = Base64ToPdf(attachment.base64, attachment.name);
        let obj = {
          attachmentType: attachment.attachmentType.toString(),
          base64: attachment.base64,
          contentType: attachment.contentType,
          relatedItemCode: attachment.relatedItemCode,
          name: attachment.name,
          size: file.size,
        };
        this.sourceAttachments.push(this.fb.group(obj));

        if (
          attachment.attachmentType == AttachmentsTypes.manufacturerCertificate
        ) {
          this.manufacturerCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.customImportPermits
        ) {
          this.customImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.shipperImportPermits
        ) {
          this.shipperImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.charCertificates
        ) {
          this.charCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.otherDocumnets
        ) {
          this.otherDocumnets.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.endUserCertificate
        ) {
          this.endUserCertificate.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.sourceTagImage
        ) {
          this.sourceTagImages.push(file);
        }
      });
    }

    this.ItemForm.patchValue({
      nuclearRelatedItem: hierarchyCode,
      isTechnologyAndSoftware: this.updateObj.isTechnologyAndSoftware,
      description: this.updateObj.description,
      status: status,
      manufacturerSerialNo: this.updateObj.manufacturerSerialNom,
      manufacturerId: manufacturer,
      manufacturerCountryId: mfCountry,
      noManufacturerCertificateFlag:
        this.updateObj.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: this.updateObj.noCustomImportPermitFlag,
      noShipperImportPermitFlag: this.updateObj.noShipperImportPermitFlag,
      noCharacterizationCertificateFlag:
        this.updateObj.noCharacterizationCertificateFlag,
      noSourceTagImageFlag: this.updateObj.noSourceTagImageFlag,
    });

    this.ItemForm.controls['relatedItemFiles'].setValue(
      this.sourceAttachments.value
    );
    this.isCustomImportPermit = !this.updateObj.noCustomImportPermitFlag;
    this.isShipperImportPermit = !this.updateObj.noShipperImportPermitFlag;
    this.isSourceTagImage = !this.updateObj.noSourceTagImageFlag;
    this.isCharCertificate = !this.updateObj.noCharacterizationCertificateFlag;
    this.isManufacturerCertificate =
      !this.updateObj.noManufacturerCertificateFlag;
    if (this.isManufacturerCertificate) {
      this.isSourceTagImage = false;
      this.isCharCertificate = false;
    }

    if (this.isCharCertificate) {
      this.isSourceTagImage = false;
      this.isManufacturerCertificate = false;
    }

    if (this.isSourceTagImage) {
      this.isCharCertificate = false;
      this.isManufacturerCertificate = false;
    }

    if (this.isView) {
      this.ItemForm.disable();
    }
  }

  preview() {
    console.log('sksjsji');
  }

  mapInputWithModel() {
    let input = this.ItemForm.value;

    let files = input.relatedItemFiles.map((f) => {
      let file: RelatedItemFiles = {
        fileId: f['fileId'] ? f['fileId'] : null,
        relatedItemCode: f['relatedItemCode'] ? f['relatedItemCode'] : null,
        name: f['name'],
        size: f['size'],
        base64: f['base64'],
        contentType: f['contentType'],
        attachmentType: f['attachmentType'],
      };

      return file;
    });

    let obj: DTORelatedItems = {
      RelatedItemCode: this.updateId ? this.updateId : null,
      ManufacturerSerialNom: input.manufacturerSerialNo,
      IsTechnologyAndSoftware: input.isTechnologyAndSoftware,
      Description: input.description,
      EntityId: '',
      FacilityId: '',
      LicenseId: '',
      PermitdetailsId: '',
      RelatedItemFiles: files,
      ManufacturerId: input.manufacturerId ? input.manufacturerId['id'] : null,
      ManufacturerCountryId: input.manufacturerCountryId
        ? input.manufacturerCountryId['id']
        : null,
      HierarchyStructureCode: input.nuclearRelatedItem
        ? input.nuclearRelatedItem['itemStructureCode']
        : null,
      StatusID: input.status ? input.status['id'] : null,
      noManufacturerCertificateFlag: input.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: input.noCustomImportPermitFlag,
      noShipperImportPermitFlag: input.noShipperImportPermitFlag,
      noSourceTagImageFlag: input.noSourceTagImageFlag,
      noCharacterizationCertificateFlag:
        input.noCharacterizationCertificateFlag,
    };

    return obj;
  }

  submit() {
    this.submitBtnClicked = true;
    let attachmentsStatus = this.validateAttachments();
    if (attachmentsStatus && this.ItemForm.valid) {
      let load = this.mapInputWithModel();
      console.log('load: ', load);
      this.relateditemsService.Add(load).subscribe((res) => {
        if (res['succeeded']) {
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('submitedSuccessfully'),
            detail: this.translateService.instant('submitedSuccessfully'),
            life: 6000,
          });
          this.router.navigate(['related-items']);
        }
      });
    } else {
      this.app.messageService.add({
        severity: 'error',
        key: 'PlanValidation',
        summary: 'Error',
        detail: this.translateService.instant('validationMessage'),
        life: 6000,
      });
    }
  }

  update() {
    this.submitBtnClicked = true;
    let attachmentsStatus = this.validateAttachments();
    if (attachmentsStatus && this.ItemForm.valid) {
      let load = this.mapInputWithModel();
      console.log('load: ', load);
      this.relateditemsService.Update(load).subscribe((res) => {
        if (res['succeeded']) {
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('updatedSuccessfully'),
            detail: this.translateService.instant('updatedSuccessfully'),
            life: 6000,
          });
          this.router.navigate(['related-items']);
        }
      });
    } else {
      this.app.messageService.add({
        severity: 'error',
        key: 'PlanValidation',
        summary: 'Error',
        detail: this.translateService.instant('validationMessage'),
        life: 6000,
      });
    }
  }
}
