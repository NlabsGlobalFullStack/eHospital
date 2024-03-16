import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { DxSchedulerModule } from 'devextreme-angular';
import { FormValidateDirective } from 'form-validate-angular';

import { UserModel } from '../../models/user.model';
import { AppointmentModel } from '../../models/appointment.model';
import { ResultModel } from '../../models/result.model';
import { AppointmentDataModel } from '../../models/appointment-data.model';
import { API_URL, PAGE_TITLE } from '../../constants/constants';
import { NavbarComponent } from '../template/navbar/navbar.component';
import { FooterComponent } from '../template/footer/footer.component';
declare const $: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent, FooterComponent, CommonModule, DxSchedulerModule, FormsModule, FormValidateDirective],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  title: string = PAGE_TITLE + "Home Page";
  appointmentsData: any[] = [];
  selectedDoctorId: string = "";
  currentDate: Date = new Date();
  doctors: UserModel[] = [];

  addModel: AppointmentModel = new AppointmentModel();
  appointmentData: AppointmentDataModel = new AppointmentDataModel();

  loginUserIsDoctor: boolean = false;

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.getAllDoctors();
  }

  getAllDoctors() {
    this.http.get(`${API_URL}appointments/getAllDoctors`).subscribe((res: any) => {
      this.doctors = res.data;
    })
  }

  getDoctorAppointments() {
    if (this.selectedDoctorId === "") return;

    this.http.get(`${API_URL}appointments/getAllByDoctorId?doctorId=${this.selectedDoctorId}`).subscribe((res: any) => {
      const data = res.data.map((val: any, i: number) => {
        return {
          id: val.id,
          text: val.patient.fullName,
          startDate: new Date(val.startDate),
          endDate: new Date(val.endDate)
        };
      });

      this.appointmentsData = data;
    })
  }

  onAppointmentFormOpening(event: any) {
    this.appointmentData = event.appointmentData;
    const doctorName = this.doctors.find(p => p.id == this.selectedDoctorId)?.fullName;
    const specialtyName = this.doctors.find(p => p.id == this.selectedDoctorId)?.doctorDetail?.specialtyName;

    this.appointmentData.doctorName = `${doctorName} - ${specialtyName}`
    event.cancel = true;
    $("#addAppointmentModal").modal('show');
  }

  add(form: NgForm) {
    if (form.valid) {
      const patientId = this.addModel.patient.id === "" ? null : this.addModel.patient.id;
      const data = {
        "doctorId": this.selectedDoctorId,
        "patientId": patientId,
        "firstName": this.addModel.patient.firstName,
        "lastName": this.addModel.patient.lastName,
        "fullAddress": this.addModel.patient.fullAddress,
        "email": this.addModel.patient.email,
        "phoneNumber": this.addModel.patient.phoneNumber,
        "identityNumber": this.addModel.patient.identityNumber,
        "dateOfBirth": this.addModel.patient.dateOfBirth,
        "bloodType": this.addModel.patient.bloodType,
        "startDate": this.appointmentData.startDate,
        "endDate": this.appointmentData.endDate,
        "price": this.doctors.find(p => p.id == this.addModel.doctorId)?.doctorDetail?.price
      };

      this.http.post(`${API_URL}appointments/create`, data).subscribe(res => {
        $("#addAppointmentModal").modal('hide');
        this.getDoctorAppointments();
        this.addModel = new AppointmentModel();
      });
    }
  }

  findPatientByIdentityNumber() {
    if (this.addModel.patient.identityNumber.length < 11) return;
    this.http.post<ResultModel<UserModel>>
      (`${API_URL}Appointments/findPatientByIdentityNumber`,
        { identityNumber: this.addModel.patient.identityNumber }).subscribe((res) => {
          if (res.data !== undefined && res.data !== null) {
            this.addModel.patient = res.data;
          }
        });
  }

  onAppointmentDeleting(event: any) {
    console.log(event);
    
    event.cancel = true;
    const result = confirm("You want to delete this appointment?");
    if (result) {
      const id = event.appointmentData?.id;
      this.http.get(`${API_URL}appointments/deleteBytId?id=${id}`).subscribe(res => {
        this.getDoctorAppointments();
      });
    }
  }
}
