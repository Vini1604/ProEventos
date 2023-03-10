import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

  @Input() titulo: string = 'Eventos';
  @Input() iconClass: string = 'fa fa-user';
  @Input() subtitulo: string = 'Desde 2023';
  @Input() botaoListar: boolean = false;

  constructor() { }

  ngOnInit(): void{
  }

}
