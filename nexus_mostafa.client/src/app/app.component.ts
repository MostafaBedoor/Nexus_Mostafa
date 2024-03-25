import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface User {
  id: number;
  name: string;
  age: number;
  active: boolean;
  last_login: Date;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public users: User[] = [];
  public ageAvg: number = 0;
  public activeUsers: number = 0;
  public inactiveUsers: number = 0;

  public searchField: string = "";
  public searchValue: string = "";

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.http.get<User[]>('https://localhost:7003/user').subscribe(
      (result) => {
        this.users = result;
        this.calculate();
      },
      (error) => {
        console.error(error);
      }
    );
  }

  calculate() {
    var sum = 0;
    var active = 0;
    var inactive = 0;
    for (var i = 0; i < this.users.length; i++) {
      sum += this.users[i].age;
      if (this.users[i].active)
        active++;
      else inactive++;
    }
    var avg = sum / this.users.length;
    this.ageAvg = avg;
    this.activeUsers = active;
    this.inactiveUsers = inactive;
  };


  public search(searhField: string, searchValue: string) {
    this.http.get<User[]>('https://localhost:7003/api/userAPI/search/' + searhField + '/' + searchValue).subscribe(
      (result) => {
        this.users = result;
        this.calculate();
      },
      (error) => {
        console.error(error);
      }
    );
  }

  public sort() {
    this.users.sort((a, b) => {
      if (a.last_login > b.last_login) {
        return 1;
      } else if (a.last_login < b.last_login) {
        return -1;
      } else {
        return 0;
      }
    });
  }

  title = 'nexus_mostafa.client';
}
