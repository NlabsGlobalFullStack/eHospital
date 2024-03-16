import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { API_URL } from '../../constants/constants';
import { FormValidateDirective } from 'form-validate-angular';
import { MessageService } from 'primeng/api';
import { ErrorService } from '../../services/error.service';
import { Toast, ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, FormValidateDirective, ToastModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements AfterViewInit {
  @ViewChild("password") password: ElementRef<HTMLInputElement> | undefined;
  isShowPassword: boolean = false;
  loginModel: LoginModel = new LoginModel();

  constructor(
    private http: HttpClient,
    private router: Router,
    private error: ErrorService,
    private primeng: MessageService) { }


  ngAfterViewInit(): void {
    if (this.primeng && this.primeng instanceof Toast) {
      setTimeout(() => {
        this.primeng.clear(); // Toast mesajını sil
      }, 4000); // 3000 milisaniye = 3 saniye
    }
  }

  showOrHidePassword() {
    this.isShowPassword = !this.isShowPassword;

    if (this.isShowPassword) {
      this.password!.nativeElement.type = "text";
    } else {
      this.password!.nativeElement.type = "password";
    }
  }

  login(form: NgForm) {
    if (form.valid) {
      this.http.post(`${API_URL}auth/login`, this.loginModel).subscribe({
        next: (res: any) => {
          localStorage.setItem("token", res.data.token);
          this.primeng.add({ severity: "warn", detail: "Giriş işlemi başarılı", summary: "Başarılı" });
          this.router.navigateByUrl("/");
        },
        error: (err: HttpErrorResponse) => this.error.errorHandler(err)
      })
    }
  }

}
