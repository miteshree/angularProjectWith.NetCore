import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from "@angular/common/http";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do'; // debug
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { forEach } from "@angular/router/src/utils/collection";
// To inject the dependancies in the service
@Injectable()
export class AccountService {
  public accountList: Observable<Account[]>;
  private _accountList: BehaviorSubject<Account[]>;
  private baseUrl: string;
  private dataStore: {
    accountList: Account[];
  };

  //// Constructor to set the values
  constructor(private http: HttpClient) {
    // Base URL for the API
    this.baseUrl = '/api/Accounts';
    this.dataStore = { accountList: [] };
    this._accountList = <BehaviorSubject<Account[]>>new BehaviorSubject([]);
    this.accountList = this._accountList.asObservable();
  }

  // Method to get all accounts by calling /api/Accounts/Get
  getAll() {
    this.http.get(`${this.baseUrl}/Get/Duplicate`)
      .map(response => response as Account[] || [])
      .subscribe(data => {
        this.dataStore.accountList = data;
        // Adding newly added Account in the list
        this._accountList.next(Object.assign({}, this.dataStore).accountList);
      }, error => console.log('Could not load account.'));
  }
}

export class Account {
  public Id: number;
  public name: string;
  public website: string;
  public telephone: string;
  public address: string;
  public contactId: number;
  public contactName: string;
} 
