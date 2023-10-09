import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-quick-action',
  templateUrl: './quick-action.component.html',
  styleUrls: ['./quick-action.component.css'],
})
export class QuickActionComponent implements OnInit {
  control = new FormControl('');
  streets: string[] = [
    'Champs-Élysées',
    'Lombard Street',
    'Abbey Road',
    'Fifth Avenue',
  ];
  filteredStreets: Observable<string[]>;

  ngOnInit() {
    this.filteredStreets = this.control.valueChanges.pipe(
      startWith(''),
      map((value) => this._filter(value || ''))
    );
  }

  private _filter(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.streets.filter((street) =>
      this._normalizeValue(street).includes(filterValue)
    );
  }

  private _normalizeValue(value: string): string {
    return value.toLowerCase().replace(/\s/g, '');
  }

  favlist() {
    document.getElementById('favDropDownList').classList.toggle('show');
  }

  filterFavList() {
    var input, filter, ul, li, a, i, div;
    input = document.getElementById('myInput');
    filter = input.value.toUpperCase();
    div = document.getElementById('favDropDownList');
    a = div.getElementsByTagName('a');
    for (i = 0; i < a.length; i++) {
      var txtValue = a[i].textContent || a[i].innerText;
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        a[i].style.display = '';
      } else {
        a[i].style.display = 'none';
      }
    }
  }

  // private _filter(value: string): string[] {
  //   const filterValue = this._normalizeValue(value);
  //   return this.streets.filter((street) =>
  //     this._normalizeValue(street).includes(filterValue)
  //   );
  // }

  // private _normalizeValue(value: string): string {
  //   return value.toLowerCase().replace(/\s/g, '');
  // }
}
