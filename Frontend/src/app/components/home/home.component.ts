import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DxSchedulerModule } from 'devextreme-angular';
import { UserModel } from '../../models/user.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [DxSchedulerModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  appointmentsData: any[] = [];
  selectedDoctorId: string = "";
  title = "Test";

  doctors: UserModel[] = [];

  currentDate: Date = new Date();
  constructor(private http: HttpClient) { }

  getAllDoctors() {
    this.http.get(`https://localhost:7149/api/Doctors/GetAllDoctors
    `).subscribe((res: any) => {
      this.doctors = res.data;
    })
  }

  getDoctorAppointments() {
    if (this.selectedDoctorId === "") return;
    this.http.get(`https://localhost:7149/api/Doctors/GetAllDoctors
    =${this.selectedDoctorId}`).subscribe((res: any) => {
      console.log(res.data);
      const data = res.data.map((val: any, i: number) => {
        return {
          text: val.patient.fullName,
          startDate: new Date(val.startDate),
          endDate: new Date(val.endDate)
        };
      });

      this.appointmentsData = data;
    })
  }
}
