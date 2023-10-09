import {
  Component,
  forwardRef,
  OnInit,
  Inject,
  Input,
  ViewChild,
} from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faPlus, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { SourceStatus, SourceTypes } from 'src/app/Enumerations/sourceTypes';
import { DateLessThanToday } from 'src/app/helper/validations/DateLessThanToday';
import { AddNewSourceModel } from 'src/app/models/addNewSourceModel/addNewSourceModel';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { InsertNewRecordService } from './../../../services/Insert-New-Recored/insert-new-record.service';
import { AttachmentsTypes } from 'src/app/Enumerations/attachments';
import { currentActivty } from 'src/app/helper/calculations';
import { Base64ToPdf, PdfToBase64 } from 'src/app/helper/fileValidation';
import { AppComponent } from 'src/app/app.component';
import { TranslateService } from '@ngx-translate/core';
import {
  TransactionAttributes,
  TransactionTypes,
} from 'src/app/Enumerations/TransactionTypes';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemSourcesProfileService } from 'src/app/services/ItemSourcesProfile/item-sources-profile.service';
import { formatDate } from '@angular/common';
import { EditSubmittedTransactionModel } from 'src/app/models/EditSubmittedTransactionModel/EditSubmittedTransactionModel';
import { TransactionService } from 'src/app/services/TransactionService/TransactionService.service';

@Component({
  selector: 'app-unsealed-transaction-edit',
  templateUrl: './unsealed-transaction-edit.component.html',
  styleUrls: ['./unsealed-transaction-edit.component.css'],
})
export class UnsealedTransactionEditComponent implements OnInit {
  // On update attribute
  onUpdateAttribute: number;
  oldValue: any;
  newValue: any;
  purpose: string;
  // Transaction ID
  transactionId: string;
  // Object
  source: any;
  // Icons
  faPlus = faPlus;
  faXmark = faXmark;

  // Enumerations
  SourceTypes = SourceTypes;
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

  // Disable Dates after today
  currentDate: any = new Date();

  // Source Variables and Attachments
  sourceFiles: File[] = [];

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  images: File[] = [];
  charCertificates: File[] = [];
  otherDocumnets: File[] = [];
  sourceTagImages: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;
  isImagesValid: boolean = true;
  isCharCertificateValid: boolean = true;
  isSourceTagImagesValid: boolean = true;

  // Attachments
  selectedSourceAttachments?: any[] = [];
  selectedMaterialAttachments?: any[] = [];
  isSourceAttachmentValid: boolean = true;
  // Form
  addNewSourceForm = this.fb.group({
    transactionType: [
      TransactionTypes.NewSourceFromImport,
      [Validators.required],
    ],
    entity: [{ value: null, disabled: true }, Validators.required],
    facility: [{ value: null, disabled: true }, Validators.required],
    licenseNumber: [{ value: null, disabled: true }, Validators.required],
    permitNumber: [{ value: null, disabled: true }, Validators.required],
    sourceType: [this.SourceTypes.unsealed],
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
        initialActivityDate: [
          { value: null, disabled: true },
          [Validators.required, DateLessThanToday],
        ],
      }),
    ]),
    status: [{ value: null, disabled: true }, Validators.required],
    facilitySourceID: [{ value: null, disabled: true }],
    associatedEquipment: [{ value: null, disabled: true }, Validators.required],
    sourceAttachments: this.fb.array([]),
    physicalForm: [{ value: null, disabled: true }, Validators.required],
    quantity: [{ value: null, disabled: true }, Validators.required],
    purpose: [{ value: null, disabled: true }],
    sourceActivity: [{ value: null, disabled: true }],
    sourceActivityUnit: [{ value: null, disabled: true }],
    numberOfConsumedSources: [{ value: null, disabled: true }],
    selectedMixOrNot: [{ value: null, disabled: true }],
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
    noSourceTagImageFlag: [
      { value: true, disabled: true },
      Validators.required,
    ],
    sourceModel: [
      { value: '', disabled: true },
      [Validators.required, Validators.minLength(3), Validators.maxLength(30)],
    ],
  });

  isManufacturerCertificate: boolean = true;
  isCharCertificate: boolean = false;
  isCustomImportPermit: boolean = true;
  isShipperImportPermit: boolean = true;
  isImage: boolean = true;
  isSourceTagImage = false;

  constructor(
    private sharedService: SharedService,
    private fb: FormBuilder,
    private insertNewRecordService: InsertNewRecordService,
    private router: Router,
    private itemSourcesProfileService: ItemSourcesProfileService,
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private route: ActivatedRoute,
    private transactionService: TransactionService
  ) {
    this.route.queryParams.subscribe((params) => {
      this.transactionId = params['id'];
    });
  }

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    // get lookups
    this.insertNewRecordService.getSourceFormsLookup().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
      });
      this.getSourceById();
    });
    // get entities
    this.insertNewRecordService.getEntities().subscribe((res) => {
      this.entities = res['data'];
    });

    // get radionuclides
    this.insertNewRecordService.getRadionulicdes().subscribe((res) => {
      this.radionulcidesList = res['data'];
      console.log(this.radionulcidesList);
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
          this.setUpdateData(res['data']);
        }
      });
  }

  setUpdateData(data) {
    console.log(this.lookups);
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
    console.log('radionuclides', radionuclides);
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

    let pf = data.physicalForm
      ? this.lookups['PhysicalForm'].find(
          (x) => x.lookupSetTermId == data.physicalForm
        )
      : null;

    let license = data.licenseId
      ? this.licenses.find((x) => x.id == data.licenseId)
      : null;

    if (data.transactionAttactcments?.length > 0) {
      data.transactionAttactcments?.map((attachment) => {
        console.log('attachment', attachment);
        let file = Base64ToPdf(
          attachment.fileBytes,
          attachment.fileOriginalName
        );
        let obj = {
          attachmentType: attachment.attachmentType.toString(),
          base64: attachment.fileBytes,
          contentType: attachment.contentType,
          fileSourceID: '',
          name: attachment.fileOriginalName,
          size: file.size,
        };

        if (
          attachment.attachmentType == AttachmentsTypes.manufacturerCertificate
        )
          this.manufacturerCertificates.push(file);
        else if (
          attachment.attachmentType == AttachmentsTypes.customImportPermits
        )
          this.customImportPermits.push(file);
        else if (attachment.attachmentType == AttachmentsTypes.charCertificates)
          this.charCertificates.push(file);
        else if (
          attachment.attachmentType == AttachmentsTypes.shipperImportPermits
        )
          this.shipperImportPermits.push(file);
        else if (attachment.attachmentType == AttachmentsTypes.otherDocumnets)
          this.otherDocumnets.push(file);
        else if (attachment.attachmentType == AttachmentsTypes.images)
          this.images.push(file);
        else if (attachment.attachmentType == AttachmentsTypes.sourceTagImage) {
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
      productionDate: formatDate(data.initialActivityDate, 'yyyy-MM-dd', 'en'),
      status: status ? status : '',
      facilitySourceID: data.facilitySerialNo,
      associatedEquipment: ac ? ac : '',
      physicalForm: pf ? pf : '',
      quantity: data.quantity,
      sourceModel: data.sourceModel,
      noManufacturerCertificateFlag: data.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: data.noCustomImportPermitFlag,
      noShipperImportPermitFlag: data.noShipperImportPermitFlag,
      noImagesFlag: data.noImagesFlag,
      noCharacterizationCertificateFlag: data.noCharacterizationCertificateFlag,
      noSourceTagImageFlag: data.noSourceTagImageFlag,
    });

    this.isCustomImportPermit = !data.noCustomImportPermitFlag;
    this.isShipperImportPermit = !data.noShipperImportPermitFlag;
    this.isImage = !data.noImagesFlag;
    this.isSourceTagImage = !data.noSourceTagImageFlag;
    this.isCharCertificate = !data.noCharacterizationCertificateFlag;
    this.isManufacturerCertificate = !data.noManufacturerCertificateFlag;
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

  // Get Licenses by Facility
  getLicensesByFacilityId(id: string, updateIds?: number[]) {
    this.insertNewRecordService.getLicenses(id).subscribe((res) => {
      this.licenses = res['data'];
      let license = this.licenses.find((x) => x.licenseId == updateIds[0]);
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
            this.getSourceById();
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
}
