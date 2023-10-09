import {
  Component,
  OnInit,
  ViewChild,
  Input,
  Output,
  EventEmitter,
} from '@angular/core';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DatatableService } from './datatable.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { faTrash, faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { catchError, map, Subscription } from 'rxjs';


export interface ICustomFilter {
  searchString?: string;
  dynamicCols: number;
  pageSize: number;
  column: string;
  sort: string;
  pageNumber: number;
  enablePagination: number;
}


export interface IGrid {
  totalRows: number;
  totalPages: number;
  data: any[];
  columns: string[];
}

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.css'],
})
export class DatatableComponent implements OnInit {
  @Input() url: string;
  @Output() editRowEvent = new EventEmitter<any>();
  @Output() deleteRowEvent = new EventEmitter<any>();
  length;

  faTrash = faTrash;
  faPen = faPenToSquare;

  isLoading = false;
  isRateLimitReached = false;

  action = "edit"

  editEvent(load) {
    this.editRowEvent.emit(load);
  }
  deleteEvent(id) {
    this.deleteRowEvent.emit(id);
  }
  @Input() displayedColumns: any[];
  dataSource = new MatTableDataSource<any>([]);
  data: any[];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.post(this.url, {}).subscribe((response: any) => {
      console.log('res', response);
      this.length = response.data.length
      this.dataSource = response.data;
    })
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    console.log("ijijijiji");
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  pageChanged(event: any) {
    console.log(event);
  }

  //@Input() url: string;
  //@Input() settings: any;
  //@Output() editRowEvent = new EventEmitter<any>();
  //@Output() deleteRowEvent = new EventEmitter<any>();

  //editEvent(load) {
  //  this.editRowEvent.emit(load);
  //}

  //deleteEvent(id) {
  //  this.deleteRowEvent.emit(id);
  //}

  //faTrash = faTrash;
  //faPen = faPenToSquare;

  //isLoading = false;
  //isRateLimitReached = false;

  //action = "edit"

  ///***************************************************/
  //@Input() displayedCols: any;
  //totalRecords: number;
  //inputFilter: ICustomFilter;
  //loadingRecords: boolean;
  //private _subscriptions: Subscription[];
  //private _timeoutId: any;
  //private _GridData: IGrid;

  //dataSource!: any[];
  //data: any;

  //@ViewChild(MatPaginator) paginator!: MatPaginator;
  //@ViewChild(MatSort) sort!: MatSort;


  //keyword: any = '';
  //sortByField: any;
  //sortByOrder: any;
  //page: any;

  //constructor(private http: HttpClient) {
  //  this.dataSource = [];
  //  this.displayedCols = [];
  //  this.totalRecords = 0;
  //  this.loadingRecords = false;
  //  this._subscriptions = [];
  //  this.inputFilter = { column: 'rowId', dynamicCols: 0, enablePagination: 1, pageNumber: 1, pageSize: 5, sort: 'desc', searchString: '' };
  //}

  //ngOnInit(): void {
  //  this._filterGridData({
  //    ...this.inputFilter
  //  });
  //}

  //ngAfterViewInit(): void {
  //  this.sort.sortChange.subscribe(sort => {
  //    this.onColumnSort(sort);
  //  });
  //  this.paginator.page.subscribe(page => {
  //    this.onPagination(page);
  //  })
  //}

  //onColumnSort(sort: Sort) {
  //  this.inputFilter = { ...this.inputFilter, column: sort.active, sort: sort.direction };
  //  this._filterGridData(this.inputFilter);
  //}

  //onPagination(page: PageEvent) {
  //  this.inputFilter = { ...this.inputFilter, pageNumber: page.pageIndex + 1, pageSize: page.pageSize };
  //  this._filterGridData(this.inputFilter);
  //}

  //onSearchString() {
  //  clearTimeout(this._timeoutId);

  //  if ((this.inputFilter.searchString?.length || 0) >= 3)
  //    this._timeoutId = setTimeout(() => {
  //      this._filterGridData({ ...this.inputFilter });
  //    }, 500);
  //}

  //onResetSearch() {
  //  this._filterGridData(this.inputFilter);
  //}

  //onKeyDown() {
  //  clearTimeout(this._timeoutId);
  //}

  //serialize(input: any) {
  //  var str = [];
  //  for (const p in input)
  //    if (input.hasOwnProperty(p)) {
  //      str.push(encodeURIComponent(p) + '=' + encodeURIComponent(input[p]));
  //    }
  //  return str.join('&');
  //}

  //loadData(input: ICustomFilter) {
  //  return this.http
  //    .post(`${this.url}?${this.serialize(this.inputFilter)}`, {})
  //    .pipe(map(apiResponse => {
  //      this._GridData = apiResponse['data'] as IGrid;
  //      console.log(this._GridData);
  //      return this._GridData;
  //    }));
  //}

  //private _filterGridData(input: ICustomFilter) {
  //  this.loadingRecords = true;
  //  this._subscriptions.push(
  //    this.loadData(input)
  //      .pipe(catchError(error => {
  //        this.dataSource = [];
  //        this.totalRecords = 0;
  //        this.loadingRecords = false;
  //        throw new Error(error);
  //      }))
  //      .subscribe(response => {
  //        this.loadingRecords = false;
  //        this.dataSource = response.data;
  //        this.displayedCols = response.columns;
  //        this.totalRecords = response.totalRows;
  //      })
  //  );
  //}

  //ngOnInit(): void {
  //  this.getData(1, 5);
  //}

  //ngAfterViewInit() {
  //  this.sort.sortChange.subscribe((sort: any) => {
  //    this.sortByField = sort.active;
  //    this.sortByOrder = sort.direction;
  //    this.dataSource.paginator.firstPage();
  //    this.getData(1, 10, sort.active, sort.direction, this.keyword);
  //  });
  //}

  //applyFilter(event: Event) {
  //  this.keyword = (event.target as HTMLInputElement).value;
  //  this.dataSource.paginator.firstPage();
  //  this.getData(1, 5, '', '', this.keyword);
  //}

  //getData(pageNumber = 1, pageSize = 5, sortByField = '', sortByOrder = '', search = '') {

  //  let params = new HttpParams();
  //  params = params.set('pageNumber', pageNumber);
  //  params = params.set('pageSize', pageSize);
  //  //params = params.set('_sort', sortByField);
  //  //params = params.set('_order', sortByOrder);
  //  //params = params.set('q', search);

  //  this.http.post(this.url + params.toString(), {}).subscribe((response: any) => {
  //    this.isRateLimitReached = response.data === null;
  //    this.data = response.data;

  //    console.log(this.data);
  //    this.data.length = pageSize * response.totalPages;

  //    this.dataSource = new MatTableDataSource<any>(this.data);
  //    this.dataSource.paginator = this.paginator;
  //  });
  //}

  //getNextData(
  //  currentSize,
  //  pageNumber = 1,
  //  pageSize = 5,
  //  sortByField = '',
  //  sortByOrder = '',
  //  search = ''
  //) {

  //  let params = new HttpParams();
  //  params = params.set('pageNumber', pageNumber);
  //  params = params.set('pageSize', pageSize);
  //  //params = params.set('_sort', sortByField);
  //  //params = params.set('_order', sortByOrder);
  //  //params = params.set('q', search);

  //  this.http.post(this.url + params.toString(), {}).subscribe((response: any) => {
  //    console.log('res', response);

  //    this.isRateLimitReached = response === null;


  //    this.data.length = currentSize;
  //    this.data = [...this.data, ...response.data]
  //    console.log('next:', this.data);


  //    this.dataSource = new MatTableDataSource<any>(this.data);
  //    this.dataSource._updateChangeSubscription();

  //    this.dataSource.paginator = this.paginator;

  //  });
  //}

  //pageChanged(event) {
  //  let pageIndex = event.pageIndex;
  //  let pageSize = event.pageSize;

  //  console.log("**********")
  //  console.log(pageIndex);
  //  console.log(pageSize);

  //  let previousIndex = event.previousPageIndex;

  //  let previousSize = pageSize * pageIndex;
  //  let pageNumber = pageIndex;
  //  console.log(event);

  //  this.getNextData(
  //    previousSize,
  //    pageNumber,
  //    pageSize,
  //  );
  //}
  //openedChange(opened: any) {
  //  console.log(opened ? 'opened' : 'closed');
  //}

  edit(load) {
    console.log("load", load)
  }
  ngOnDestroy(): void { }
}
