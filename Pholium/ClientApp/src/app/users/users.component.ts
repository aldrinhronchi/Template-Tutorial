import { Component, OnInit } from '@angular/core';
import {UserDataService} from "../_data-services/user.data-service";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: any[] = [];
  user: any = {};
  showList: boolean = true;
  constructor(private userDataService: UserDataService) { }

  ngOnInit(): void {
    this.get();
  }
  get() {
    this.userDataService.get().subscribe((data:any) => {
      this.users = data;
      this.showList = true;
    }, error => {
      console.log(error);
      alert('erro interno do sistema');
    })
  }
  post() {
    this.userDataService.post(this.user).subscribe(data => {
    if (data) {
      alert('Usuario Cadastrado com sucesso');
      this.get();
      this.user = {};
    } else {
      alert('erro ao Cadastrar Usuario no sistema');
    }
    }, error => {
      console.log(error);
      alert('erro interno do sistema');
    })
  }
}
