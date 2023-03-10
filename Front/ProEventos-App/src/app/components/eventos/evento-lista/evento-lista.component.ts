import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  public filtrarEventos(filtroEventos: string): Evento[] {
    filtroEventos = filtroEventos.toLocaleLowerCase();
    return this.eventos.filter(
      (evento : {tema: string, local: string}) => evento.tema.toLocaleLowerCase().indexOf(filtroEventos) !== -1 || evento.local.toLocaleLowerCase().indexOf(filtroEventos) !== -1
    );
  }

  modalRef?: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public showImage = true;
  public larguraImagem: number = 50;
  public margemImagem: number = 2;
  private _filtroEventos: string = '';

  public get filtroEventos():string{
    return this._filtroEventos;
  }

  public set filtroEventos(value: string){
    this._filtroEventos = value;
    this.eventosFiltrados = this.filtroEventos ? this.filtrarEventos(this.filtroEventos) : this.eventos;
  }

  constructor(private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();

    setTimeout(() => {
      this.spinner.hide()
    }, 5000);
  }
  public getEventos():void {

    this.eventoService.getEventos().subscribe({
      next: (reponse:Evento[]) => {
        this.eventos = reponse,
        this.eventosFiltrados = this.eventos
      },
      error:(error: any) => {
        this.spinner.hide(),
        this.toastr.error('Erro ao retornar os eventos', 'Erro')
      },
      complete: () => this.spinner.hide()
    });

  }


  openModal(template: TemplateRef<any>): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('O evento foi deletado!!', 'Sucesso');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
