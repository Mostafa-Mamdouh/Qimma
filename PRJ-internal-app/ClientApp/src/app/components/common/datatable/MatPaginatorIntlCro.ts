import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

@Injectable()
export class MatPaginatorIntlCro extends MatPaginatorIntl {
  translate: TranslateService;
  lang: string;

  override itemsPerPageLabel = '';
}
