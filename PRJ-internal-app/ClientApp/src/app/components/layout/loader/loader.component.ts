import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../../../../services/General/loader.service';

declare var $: any;
@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css'],
})
export class LoaderComponent implements OnInit {
  loading: boolean;
  loader: any = '../../../../assets/imgs/Loader.gif';

  constructor(private loaderService: LoaderService) {}
  ngOnInit(): void {
    this.loaderService.isLoading.subscribe((v) => {
      this.loading = v;
      if (this.loading) {
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth',
        });
      }
    });
  }
}
