import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../_service/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

constructor(
  private auth: AuthService
  ){
  this.loginForm = new FormGroup({
    "login": new FormControl("",Validators.required),
    "password": new FormControl("",Validators.required)
  });
}

  submit(){
    if(this.loginForm.invalid){
      console.log("invalid form");
      return;
    }

    this.auth.login(this.loginForm.value.login, this.loginForm.value.password);
  }
}
