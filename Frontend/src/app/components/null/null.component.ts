import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { LoginResponseModel } from '../../models/login-response.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-null',
  standalone: true,
  imports: [RouterOutlet],
  template: "<router-outlet></router-outlet>",
})
export class NullComponent implements OnInit {
  
  user: LoginResponseModel = new LoginResponseModel();
  
  constructor(private router: Router, private authService: AuthService) { }
  ngOnInit(): void {
    this.user.userType = this.authService.user.userType;
    this.redirectToCorrectComponent();
  }

  redirectToCorrectComponent(): void {
    debugger
    console.log("User Type:", this.user.userType);
  
    switch (this.user.userType) {
      case "User":
        console.log("User type is User");
        this.router.navigateByUrl('/employee');
        break;
      case "Doctor":
        console.log("User type is Doctor");
        this.router.navigateByUrl('/doctor');
        break;
      case "Patient":
        console.log("User type is Patient");
        this.router.navigateByUrl('/patient');
        break;
      case "Nurse":
        console.log("User type is Nurse");
        this.router.navigateByUrl('/nurse');
        break;
      case "Admin":
        console.log("User type is Admin");
        this.router.navigateByUrl('/admin');
        break;
      default:
        console.log("Unknown user type");
        this.router.navigateByUrl('/');
        break;
    }
  }
  
}
