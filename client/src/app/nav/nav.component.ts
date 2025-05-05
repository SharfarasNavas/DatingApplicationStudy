import { Component, inject } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
 model: any ={};
accountServices = inject(AccountService);
//  loggedIn = false;
 login(){
  console.log(this.model);
  this.accountServices.login(this.model).subscribe({
    next: response => {
      console.log("loggedIn",response);
    },
    error: error => console.log("error", error)  
  })
 }
 logout(){
  this.accountServices.logout();
}
}
