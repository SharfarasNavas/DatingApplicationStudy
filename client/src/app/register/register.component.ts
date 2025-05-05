import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService)
   @Input() UsersfromHomeComponent : any;
   //UsersfromHomeComponent = input<any>();
   @Output() cancelRegister = new EventEmitter();
   //cancelRegister = output<boolean>();
  model: any ={}
  register(model: any){
    console.log(this.model); 
    this.accountService.register(model).subscribe({
      next: response =>{
        console.log(response);
        this.cancel();
      },
      error: e => console.log(e)   
    })
  }

  cancel(){
    console.log('cancelled');
    this.cancelRegister.emit(false);
    
  }
}
