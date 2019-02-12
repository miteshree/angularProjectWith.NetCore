import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/catch';
import { AccountService } from "./accountService";
import { Observable } from "rxjs/Observable";
import { Account } from "./accountService"
import { FilterPipe } from '../filter.pipe';

@Component({
  selector: 'account',
  providers: [AccountService],
  templateUrl: './account.html'
})
export class AccountComponent implements OnInit {
  public accountList: Observable<Account[]>;
  searchText: string;

  hide = false;
  myName: string;
  account: Account;
  constructor(private dataService: AccountService) {
    this.account = new Account();
  }
  // if you want to debug info  just uncomment the console.log lines.  
  ngOnInit() {
    //    console.log("in ngOnInit");  
    this.accountList = this.dataService.accountList;
    this.dataService.getAll();
      
    }
  }
