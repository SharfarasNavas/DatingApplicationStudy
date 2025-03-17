import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'Dating App';
  users: any;
  http = inject(HttpClient);
  ngOnInit(): void {
    this.http.get("https://localhost:5001/api/User").subscribe({
      next: (response) => {     
        this.users = response;
        console.log(this.users);
      }
      ,
      error: () => console.log("error"),
      complete: () => console.log("gi")     
      }
    );
  }

}
