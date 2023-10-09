import {
  Component,
  forwardRef,
  OnInit,
  Inject,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { ShieldingMaterial } from 'src/app/Enumerations/shieldingMaterials';
import { SourceTypes, SourceStatus } from 'src/app/Enumerations/sourceTypes';
import { DateLessThanToday } from 'src/app/helper/validations/DateLessThanToday';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { InsertNewRecordService } from './../../../services/Insert-New-Recored/insert-new-record.service';
import { AttachmentsTypes } from 'src/app/Enumerations/attachments';
import { TranslateService } from '@ngx-translate/core';
import { AppComponent } from 'src/app/app.component';
import {
  TransactionAttributes,
  TransactionTypes,
} from 'src/app/Enumerations/TransactionTypes';
import { ActivatedRoute } from '@angular/router';
import { formatDate } from '@angular/common';
import { TransactionService } from './../../../services/TransactionService/TransactionService.service';
import { Base64ToPdf, PdfToBase64 } from 'src/app/helper/fileValidation';
import { EditSubmittedTransactionModel } from 'src/app/models/EditSubmittedTransactionModel/EditSubmittedTransactionModel';
import { NuclearMaterialServices } from 'src/app/services/nuclearMaterial/nuclear-material.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-sealed-transaction-edit',
  templateUrl: './sealed-transaction-edit.component.html',
  styleUrls: ['./sealed-transaction-edit.component.css'],
})
export class SealedTransactionEditComponent implements OnInit {
  shieldCodeList: any[] = [];

  // On update attribute
  onUpdateAttribute: number;
  oldValue: any;
  newValue: any;
  purpose: string;
  // Transaction ID
  transactionId: string;

  // Object
  source: any;
  // radionuclides: any[] = [];
  isShieldDU = false;

  // Icons
  faPlus = faPlus;
  faXmark = faXmark;

  // Enumerations
  SourceTypes = SourceTypes;
  ShieldingMaterial = ShieldingMaterial;
  AttachmentsTypes = AttachmentsTypes;
  TransactionTypes = TransactionTypes;
  TransactionAttributes = TransactionAttributes;

  // Language
  lang: string;
  langServiceSupscribtion: Subscription;

  // Lookups and DropDowns
  lookups?: any = {};
  statusLookup?: any[];
  entities?: any[] = [];
  facilities?: any[] = [];
  licenses?: any[] = [];
  permits?: any[] = [];
  radionulcidesList?: any[] = [];

  // Source Variables and Attachments
  sourceFiles: File[] = [];

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  otherDocumnets: File[] = [];
  images: File[] = [];
  sourceTagImages: File[] = [];
  charCertificates: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;
  isImagesValid: boolean = true;
  isCharCertificateValid: boolean = true;
  isSourceTagImagesValid: boolean = true;

  // Shield variables and Attachments
  isShieldDUSwitch = false;

  shieldFiles: File[] = [];

  shieldManufacturerCertificates: File[] = [];
  shieldCustomImportPermits: File[] = [];
  shieldShipperImportPermits: File[] = [];
  shieldOtherDocumnets: File[] = [];
  shieldImages: File[] = [];

  isShieldManufacturerCertificateValid: boolean = true;
  isShieldCustomImportPermitsValid: boolean = true;
  isShieldShipperImportPermitsValid: boolean = true;
  isShieldImagesValid: boolean = true;

  // Attachments
  selectedSourceAttachments?: any[] = [];
  selectedMaterialAttachments?: any[] = [];
  isShieldAttachmentValid: boolean = true;
  isSourceAttachmentValid: boolean = true;

  // Form
  addNewSourceForm = this.fb.group({
    transactionType: [
      { value: TransactionTypes.NewSourceFromImport, disabled: true },
      [Validators.required],
    ],
    entity: [{ value: null, disabled: true }, Validators.required],
    facility: [{ value: null, disabled: true }, Validators.required],
    licenseNumber: [{ value: null, disabled: true }, Validators.required],
    permitNumber: [{ value: null, disabled: true }, Validators.required],
    sourceType: [this.SourceTypes.sealed],
    manufacturerSerialNo: [
      { value: null, disabled: true },
      Validators.required,
    ],
    manufacturer: [{ value: null, disabled: true }],
    manufacturerCountry: [{ value: null, disabled: true }, Validators.required],
    productionDate: [
      { value: null, disabled: true },
      [Validators.required, DateLessThanToday],
    ],
    radionuclides: this.fb.array([
      this.fb.group({
        radionuclide: [{ value: null, disabled: true }, [Validators.required]],
        initialActivity: [
          { value: null, disabled: true },
          [Validators.required],
        ],
        activityUnit: [{ value: null, disabled: true }, [Validators.required]],
      }),
    ]),
    status: [{ value: null, disabled: true }, Validators.required],
    facilitySourceID: [{ value: null, disabled: true }],
    associatedEquipment: [{ value: null, disabled: true }, Validators.required],
    sourceAttachments: this.fb.array([]),
    isShieldDU: [{ value: null, disabled: true }],
    shieldNuclearMaterialCode: [{ value: null, disabled: true }],
    weightOfNuclearMaterial: [{ value: null, disabled: true }],
    weightOfNuclearMaterialUnit: [{ value: null, disabled: true }],
    enrichmentOfUranium: [{ value: null, disabled: true }],
    shieldChemicalForm: [{ value: null, disabled: true }], // If the nuclear material is DU then the value will be “Other alloys”.
    shieldAttachments: this.fb.array([]),
    selectedMixOrNot: ['single'],
    noManufacturerCertificateFlag: [
      { value: true, disabled: true },
      Validators.required,
    ],
    noCustomImportPermitFlag: [
      { value: true, disabled: true },
      Validators.required,
    ],
    noShipperImportPermitFlag: [
      { value: true, disabled: true },
      Validators.required,
    ],
    noImagesFlag: [{ value: true, disabled: true }, Validators.required],
    noCharacterizationCertificateFlag: [
      { value: true, disabled: true },
      Validators.required,
    ],
    noSourceTagImageFlag: { value: true, disabled: true },
    sourceModel: [
      { value: '', disabled: true },
      [Validators.required, Validators.minLength(3), Validators.maxLength(30)],
    ],
  });
  isChanged: boolean;

  newShieldAdded: boolean = false;
  isManufacturerCertificate: boolean = true;
  isCharCertificate: boolean = false;
  isCustomImportPermit: boolean = true;
  isShipperImportPermit: boolean = true;
  isImage: boolean = true;
  isSourceTagImage = false;

  isShieldManufacturerCertificate: boolean = true;
  isShieldCustomImportPermit: boolean = true;
  isShieldShipperImportPermit: boolean = true;
  isShieldImage: boolean = true;

  constructor(
    private sharedService: SharedService,
    private fb: FormBuilder,
    private insertNewRecordService: InsertNewRecordService,
    private transactionService: TransactionService,
    private translateService: TranslateService,
    private nuclearMaterialServices: NuclearMaterialServices,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent
  ) {
    this.route.queryParams.subscribe((params) => {
      this.transactionId = params['id'];
    });
  }

  get radionuclides() {
    return this.addNewSourceForm.get('radionuclides') as FormArray;
  }

  getSourceById() {
    this.insertNewRecordService
      .getSourceById(this.transactionId)
      .subscribe((res) => {
        console.log('BYID:', res);
        if (res['data']) {
          this.source = res['data'];
          this.getShields();
          this.setUpdateData(res['data']);
        }
      });
  }

  handleCertificateChange(choice) {
    let noManufacturerCertificate =
      this.addNewSourceForm.controls['noManufacturerCertificateFlag'];
    let noCharCertificate =
      this.addNewSourceForm.controls['noCharacterizationCertificateFlag'];
    let noSourceTag = this.addNewSourceForm.controls['noSourceTagImageFlag'];
    let noCustomImport =
      this.addNewSourceForm.controls['noCustomImportPermitFlag'];
    let noShipperImportPermit =
      this.addNewSourceForm.controls['noShipperImportPermitFlag'];
    let imagesFlag = this.addNewSourceForm.controls['noImagesFlag'];

    if (choice == 1) {
      this.isManufacturerCertificate = !noManufacturerCertificate.value;
      this.isCharCertificate = !this.isManufacturerCertificate;
      this.isSourceTagImage = false;
      noCharCertificate.setValue(false);
      noSourceTag.setValue(true);
    } else if (choice == 2) {
      this.isCharCertificate = !noCharCertificate.value;
      this.isSourceTagImage = !this.isCharCertificate;
      this.isManufacturerCertificate = false;
      noManufacturerCertificate.setValue(true);
      noSourceTag.setValue(false);
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

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    // get entities
    this.insertNewRecordService.getEntities().subscribe((res) => {
      this.entities = res['data'];
    });

    // get lookups
    this.insertNewRecordService.getSourceFormsLookup().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
      });
      this.getSourceById();
    });

    // get radionuclides
    this.insertNewRecordService.getRadionulicdes().subscribe((res) => {
      this.radionulcidesList = res['data'];
    });
  }

  setUpdateData(data) {
    let radionuclides = data.trnItemSourceRadionuclides?.map((radionuclide) => {
      return {
        radionuclide: this.radionulcidesList.find(
          (x) => x.radionuclideId == radionuclide.radionulcideId
        ),
        initialActivity: radionuclide.initialActivity,
        initialActivityUnit: radionuclide.initialActivityUnit
          ? this.lookups['ActivityUnit'].find(
              (x) => x.lookupSetTermId == radionuclide.initialActivityUnit
            )
          : '',
        initialActivityDate: radionuclide.initialActivityDate
          ? formatDate(radionuclide.initialActivityDate, 'yyyy-MM-dd', 'en')
          : null,
      };
    });
    if (radionuclides.length > 1) {
      this.addNewSourceForm.controls['selectedMixOrNot'].setValue('mix');
    }
    let radionuclidesAsFormArray = this.addNewSourceForm.get(
      'radionuclides'
    ) as FormArray;
    radionuclidesAsFormArray.clear();
    radionuclides?.forEach((radionuclide) =>
      radionuclidesAsFormArray.push(
        this.fb.group({
          radionuclide: [
            { value: radionuclide.radionuclide, disabled: true },
            [Validators.required],
          ],
          initialActivity: [
            { value: radionuclide.initialActivity, disabled: true },
            [Validators.required],
          ],
          activityUnit: [
            { value: radionuclide.initialActivityUnit, disabled: true },
            [Validators.required],
          ],
          initialActivityDate: [
            { value: radionuclide.initialActivityDate, disabled: true },
            [Validators.required, DateLessThanToday],
          ],
        })
      )
    );

    let manufacturer = data.manufacturerCountryId
      ? this.lookups['Manufacturer'].find(
          (x) => x.lookupSetTermId == data.manufacturerId
        )
      : null;

    let manufacturerCountry = data.manufacturerCountryId
      ? this.lookups['ManufacturerCountry'].find(
          (x) => x.lookupSetTermId == data.manufacturerCountryId
        )
      : null;

    let status = data.status
      ? this.lookups['Status'].find((x) => x.lookupSetTermId == data.status)
      : null;

    let ac = data.associatedEquipment
      ? this.lookups['Associated Equipment'].find(
          (x) => x.lookupSetTermId == data.associatedEquipment
        )
      : null;

    let license = data.licenseId
      ? this.licenses.find((x) => x.id == data.licenseId)
      : null;

    if (data.transactionAttactcments?.length > 0) {
      data.transactionAttactcments?.map((attachment) => {
        let file = Base64ToPdf(
          attachment.fileBytes,
          attachment.fileOriginalName
        );

        if (
          attachment.attachmentType == AttachmentsTypes.manufacturerCertificate
        ) {
          attachment.forShield
            ? this.shieldManufacturerCertificates.push(file)
            : this.manufacturerCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.customImportPermits
        ) {
          attachment.forShield
            ? this.shieldCustomImportPermits.push(file)
            : this.customImportPermits.push(file);
        } else if (attachment.attachmentType == AttachmentsTypes.images) {
          attachment.forShield
            ? this.shieldImages.push(file)
            : this.images.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.charCertificates
        ) {
          if (!attachment.forShield) {
            this.charCertificates.push(file);
          }
        } else if (
          attachment.attachmentType == AttachmentsTypes.shipperImportPermits
        ) {
          attachment.forShield
            ? this.shieldShipperImportPermits.push(file)
            : this.shipperImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.otherDocumnets
        ) {
          attachment.forShield
            ? this.shieldOtherDocumnets.push(file)
            : this.otherDocumnets.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.sourceTagImage
        ) {
          this.sourceTagImages.push(file);
        }
      });
    }

    if (data?.facilityProfile) console.log(data.facilityProfile);
    this.getLicensesByFacilityId(data.facilityProfile.id, [
      data.licenseId,
      data.permitdetailsId,
    ]);

    this.addNewSourceForm.patchValue({
      entity: data?.entityProfile,
      facility: data?.facilityProfile,
      licenseNumber: license ? license : '',
      manufacturerSerialNo: data.manufacturerSerialNo,
      manufacturer: manufacturer ? manufacturer : '',
      manufacturerCountry: manufacturerCountry ? manufacturerCountry : '',
      productionDate: data.initialActivityDate
        ? formatDate(data.initialActivityDate, 'yyyy-MM-dd', 'en')
        : null,
      status: status ? status : '',
      facilitySourceID: data?.facilitySerialNo,
      associatedEquipment: ac ? ac : '',
      isShieldDU: data.isShieldDU,
      enrichmentOfUranium: data.enrichmentOfUranium,
      // shieldNuclearMaterialCode: data.shieldNuclearMaterialCode,
      shieldChemicalForm: data.shieldChemicalForm,
      sourceModel: data.sourceModel,
      noManufacturerCertificateFlag: data.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: data.noCustomImportPermitFlag,
      noShipperImportPermitFlag: data.noShipperImportPermitFlag,
      noImagesFlag: data.noImagesFlag,
      noCharacterizationCertificateFlag: data.noCharacterizationCertificateFlag,
      noSourceTagImageFlag: data.noSourceTagImageFlag,
    });

    this.addNuclearMaterial.patchValue({
      entityId: data?.entityProfile,
      facilityId: data?.facilityProfile,
    });

    this.isCustomImportPermit = !data.noCustomImportPermitFlag;
    this.isShipperImportPermit = !data.noShipperImportPermitFlag;
    this.isImage = !data.noImagesFlag;
    this.isSourceTagImage = !data.noSourceTagImageFlag;
    this.isCharCertificate = !data.noCharacterizationCertificateFlag;
    this.isManufacturerCertificate = !data.noManufacturerCertificateFlag;
  }

  // Get Licenses by Facility
  getLicensesByFacilityId(id: string, updateIds?: number[]) {
    this.insertNewRecordService.getLicenses(id).subscribe((res) => {
      this.licenses = res['data'];
      let license = this.licenses.find((x) => x.licenseId == updateIds[0]);
      console.log('license: ', license);
      this.addNewSourceForm.controls['licenseNumber'].setValue(license);
      this.getPermitsByLicenseId(license.id, updateIds);
    });
  }
  // Get Permits by License
  getPermitsByLicenseId(id: string, updateIds?: number[]) {
    this.insertNewRecordService.getPermits(id).subscribe((res) => {
      this.permits = res['data'];
      let permit = this.permits.find((x) => x.permitDetailsId == updateIds[1]);
      this.addNewSourceForm.controls['permitNumber'].setValue(permit);
    });
  }

  // Update Attribute
  updateAttribute(oldValue: any, field: any) {
    console.log('oldValue: ', oldValue);
    console.log('field: ', field);
    this.onUpdateAttribute = field;
    if (field == TransactionAttributes.status) {
      this.oldValue = this.lookups['Status'].find(
        (x) => x.lookupSetTermId == oldValue
      );
    } else if (field == TransactionAttributes.associatedEquipment) {
      this.oldValue = this.lookups['Associated Equipment'].find(
        (x) => x.lookupSetTermId == oldValue
      );
    } else if (field == TransactionAttributes.facilitySourceId) {
      this.oldValue = this.source.facilitySerialNo;
    } else if (field == TransactionAttributes.shield) {
      this.oldValue = this.shieldCodeList.find(
        (x) => x.sourceId == this.source.shieldNuclearMaterialCode
      );
    }
  }

  submit() {
    if (this.oldValue && this.newValue && this.purpose) {
      let transactionTypeId;
      let transactionAttribute;
      if (this.onUpdateAttribute == TransactionAttributes.status) {
        transactionTypeId = TransactionTypes.ChangeSourceStatus;
        transactionAttribute = TransactionAttributes.status;
        this.oldValue = this.oldValue.id;
        this.newValue = this.newValue.id;
      } else if (
        this.onUpdateAttribute == TransactionAttributes.associatedEquipment
      ) {
        transactionTypeId = TransactionTypes.ChangeAssociatedEquipment;
        transactionAttribute = TransactionAttributes.associatedEquipment;
        this.oldValue = this.oldValue.id;
        this.newValue = this.newValue.id;
      } else if (
        this.onUpdateAttribute == TransactionAttributes.facilitySourceId
      ) {
        transactionTypeId = TransactionTypes.ChangeFacilityId;
        transactionAttribute = TransactionAttributes.facilitySourceId;
      } else if (this.onUpdateAttribute == TransactionAttributes.shield) {
        transactionTypeId = TransactionTypes.ChangeSourceShield;
        transactionAttribute = TransactionAttributes.shield;
        this.oldValue = this.oldValue.sourceId;
        this.newValue = this.newValue.sourceId;
      }

      let body: EditSubmittedTransactionModel = {
        id: null,
        createdBy: null,
        createdOn: null,
        modifiedBy: null,
        modifiedOn: null,
        itemSourceProfileId: this.source.sourceId,
        transactionDate: new Date().toISOString(),
        transactionTypeId: transactionTypeId,
        transactionAttribute: transactionAttribute,
        oldValue: this.oldValue,
        newValue: this.newValue,
        remarks: this.purpose,
      };

      this.transactionService
        .editSubmittedTransaction(body)
        .subscribe((res) => {
          if (res['succeeded']) {
            this.app.messageService.add({
              severity: 'success',
              key: 'PlanValidation',
              summary: this.lang == 'en' ? res['message'] : res['messageAr'],
              detail: this.lang == 'en' ? res['message'] : res['messageAr'],
              life: 6000,
            });
          }
        });
    } else {
      this.app.messageService.add({
        severity: 'error',
        key: 'PlanValidation',
        summary:
          this.lang == 'en' ? 'Please fill all fields' : 'يرجى ملء جميع الحقول',
        detail:
          this.lang == 'en' ? 'Please fill all fields' : 'يرجى ملء جميع الحقول',
        life: 6000,
      });
    }
    this.oldValue = null;
    this.newValue = null;
    this.purpose = '';
    this.onUpdateAttribute = null;
  }
  closeEditModal() {
    this.oldValue = null;
    this.newValue = null;
    this.purpose = '';
    this.onUpdateAttribute = null;
  }

  // Shield Attachments FormArray Methods
  get shieldAttachments() {
    return this.addNuclearMaterial.get('nuclearMaterialFiles') as FormArray;
  }

  addShieldAttachments(file, attachmentType) {
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
          forShield: true,
        };
      })
      .then((res) => {
        this.shieldAttachments.push(this.fb.group(res));
      });
  }

  removeShieldAttachment(i: number) {
    this.shieldAttachments.removeAt(i);
  }
  onAddShieldModal() {
    let license = this.addNewSourceForm.controls['licenseNumber'].value;

    if (license) {
      this.getShieldPermitsByLicenseId(license['id']);
      this.addNuclearMaterial.controls['licenseId'].setValue(license);
    }
  }
  onSelectShieldFile(event, field) {
    this.shieldFiles = [];
    this.shieldFiles.push(...event.addedFiles);
    for (let i = 0; i < this.shieldFiles.length; i++) {
      this.addShieldAttachments(<File>this.shieldFiles[i], field);

      if (field == AttachmentsTypes.manufacturerCertificate)
        this.shieldManufacturerCertificates.push(this.shieldFiles[i]);
      else if (field == AttachmentsTypes.customImportPermits)
        this.shieldCustomImportPermits.push(this.shieldFiles[i]);
      else if (field == AttachmentsTypes.images)
        this.shieldImages.push(this.shieldFiles[i]);
      else if (field == AttachmentsTypes.shipperImportPermits)
        this.shieldShipperImportPermits.push(this.shieldFiles[i]);
      else this.shieldOtherDocumnets.push(this.shieldFiles[i]);
    }
  }

  onRemoveShieldFile(event, field) {
    this.shieldFiles = [];

    if (field == AttachmentsTypes.manufacturerCertificate) {
      this.shieldManufacturerCertificates.splice(
        this.shieldManufacturerCertificates.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else if (field == AttachmentsTypes.customImportPermits) {
      this.shieldCustomImportPermits.splice(
        this.shieldCustomImportPermits.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else if (field == AttachmentsTypes.images) {
      this.shieldImages.splice(this.shieldImages.indexOf(event), 1);
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else if (field == AttachmentsTypes.shipperImportPermits) {
      this.shieldShipperImportPermits.splice(
        this.shieldShipperImportPermits.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    } else {
      this.shieldOtherDocumnets.splice(
        this.shieldOtherDocumnets.indexOf(event),
        1
      );
      this.shieldFiles.push(...this.shieldManufacturerCertificates);
      this.shieldFiles.push(...this.shieldCustomImportPermits);
      this.shieldFiles.push(...this.shieldImages);
      this.shieldFiles.push(...this.shieldShipperImportPermits);
      this.shieldFiles.push(...this.shieldOtherDocumnets);
    }

    this.addNewSourceForm.controls['shieldAttachments'].clear();

    for (let i = 0; i < this.shieldManufacturerCertificates.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldManufacturerCertificates[i],
        AttachmentsTypes.manufacturerCertificate
      );
    }
    for (let i = 0; i < this.shieldCustomImportPermits.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldCustomImportPermits[i],
        AttachmentsTypes.customImportPermits
      );
    }
    for (let i = 0; i < this.shieldImages.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldImages[i],
        AttachmentsTypes.customExportPermits
      );
    }
    for (let i = 0; i < this.shieldShipperImportPermits.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldShipperImportPermits[i],
        AttachmentsTypes.shipperImportPermits
      );
    }
    for (let i = 0; i < this.shieldOtherDocumnets.length; i++) {
      this.addShieldAttachments(
        <File>this.shieldOtherDocumnets[i],
        AttachmentsTypes.otherDocumnets
      );
    }
  }

  submitNuclearClicked = false;

  shieldPremits: any[] = [];

  addNuclearMaterial = this.fb.group({
    manufacturerSerialNo: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(12)],
    ],
    initialMass: [null, Validators.required],
    isShield: [true, Validators.required],
    initialMassUnit: [null, Validators.required],
    entityId: [null, Validators.required],
    facilityId: [null, Validators.required],
    licenseId: [null, Validators.required],
    permitdetailsId: [null, Validators.required],
    nuclearMaterialFiles: this.fb.array([]),
    sourceModel: [
      '',
      [Validators.required, Validators.minLength(3), Validators.maxLength(12)],
    ],
    noManufacturerCertificateFlag: [false],
    noCustomImportPermitFlag: [false],
    noShipperImportPermitFlag: [false],
    noImagesFlag: [false],
  });

  getShieldPermitsByLicenseId(id: string) {
    this.insertNewRecordService.getPermits(id).subscribe((res) => {
      this.shieldPremits = res['data'];
    });
  }

  validateShieldAttachments() {
    if (this.isShieldManufacturerCertificate)
      this.isShieldManufacturerCertificateValid =
        this.shieldManufacturerCertificates.length == 0 ? false : true;
    else this.isShieldManufacturerCertificateValid = true;

    if (this.isShieldCustomImportPermit) {
      this.isShieldCustomImportPermitsValid =
        this.shieldCustomImportPermits.length == 0 ? false : true;
    } else this.isShieldCustomImportPermitsValid = true;

    if (this.isShieldShipperImportPermit) {
      this.isShieldShipperImportPermitsValid =
        this.shieldShipperImportPermits.length == 0 ? false : true;
    } else this.isShieldShipperImportPermitsValid = true;

    if (this.isShieldImage) {
      this.isShieldImagesValid = this.shieldImages.length == 0 ? false : true;
    } else this.isShieldImagesValid = true;

    if (
      this.isShieldManufacturerCertificateValid &&
      this.isShieldCustomImportPermitsValid &&
      this.isShieldShipperImportPermitsValid &&
      this.isShieldImagesValid
    ) {
      this.addNuclearMaterial.controls['nuclearMaterialFiles'].setErrors(null);
      this.isShieldAttachmentValid = true;
    } else {
      this.isShieldAttachmentValid = false;
    }
  }

  handleShieldCertificateChange(choice) {
    if (choice == 1) {
      this.isShieldManufacturerCertificate =
        !this.isShieldManufacturerCertificate;
    } else if (choice == 2) {
      this.isShieldCustomImportPermit = !this.isShieldCustomImportPermit;
    } else if (choice == 3) {
      this.isShieldShipperImportPermit = !this.isShieldShipperImportPermit;
    } else if (choice == 4) {
      this.isShieldImage = !this.isShieldImage;
    }
  }

  getShields() {
    this.nuclearMaterialServices
      .getNuclearMaterialsForTransaction(
        this.source.facilityProfile?.id,
        this.source?.sourceId
      )
      .subscribe((res) => {
        this.shieldCodeList = res['data'];
        let shieldCode = this.source.shieldNuclearMaterialCode
          ? this.shieldCodeList.find(
              (x) => x.sourceId == this.source.shieldNuclearMaterialCode
            )
          : null;

        this.addNewSourceForm.controls['shieldNuclearMaterialCode'].setValue(
          shieldCode
        );
      });
  }

  submitNuclearShield() {
    this.submitNuclearClicked = true;
    this.newShieldAdded = true;
    this.validateShieldAttachments();
    let body = this.addNuclearMaterial.value;
    let load = {
      manufacturerSerialNo: body.manufacturerSerialNo,
      initialMass: body.initialMass,
      isShield: body.isShield,
      initialMassUnit: body.initialMassUnit ? body.initialMassUnit['id'] : '',
      entityId: body.entityId ? body.entityId['id'] : '',
      facilityId: body.facilityId ? body.facilityId['id'] : '',
      licenseId: body.licenseId ? body.licenseId['id'] : '',
      permitdetailsId: body.permitdetailsId ? body.permitdetailsId['id'] : '',
      nuclearMaterialFiles: body.nuclearMaterialFiles,
      sourceModel: body.sourceModel,
    };
    if (this.addNuclearMaterial.valid && this.isShieldAttachmentValid) {
      this.nuclearMaterialServices.addNuclearShield(load).subscribe((res) => {
        if (res['succeeded']) {
          this.shieldCodeList.push(res['data']);
          this.app.messageService.add({
            severity: 'success',
            key: 'PlanValidation',
            summary: this.translateService.instant('Submitted Successfully'),
            detail: this.translateService.instant('Submitted Successfully'),
            life: 6000,
          });
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
