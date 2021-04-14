import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getEventDetails } from "../core/useDecoventosControl";
import UserPost from "../UserPost/UserPost";
import "./EventDetails.css";
import { format } from "date-fns";
import { Button } from "@material-ui/core";

function EventDetails() {
  const { id } = useParams();

  const [event, setEvent] = useState(null);
  const url = "/participante/" + id.toString();
  
  useEffect(() => getEventDetails(id).then((data) => setEvent(data)), []);

  if (event) {
    return (
      <div>
        <div>
          <img
            className="img-detalhe"
            src="https://blog.sympla.com.br/wp-content/uploads/2013/08/Como-a-tecnologia-est%C3%A1-revolucionando-a-produ%C3%A7%C3%A3o-de-eventos.png"
            alt="cabeçalho eventos tecnologia"
          />
        </div>
        <div className="eventos-detalhe">
          <h3>Nome do Evento: {event.nome.toString()}</h3>
          <p>Status: {event.statusEventoResponseModel.nomeStatus.toString()}</p>
          <p>
            Categoria: {event.categoriaResponseModel.nomeCategoria.toString()}
          </p>
          <p>Dia: {format(new Date(event.dataHoraInicio), "dd/MM/yyyy")}</p>
          <p>
            Horário de início: {format(new Date(event.dataHoraInicio), "kk:ss")}
          </p>
          <p>
            Horário de término: {format(new Date(event.dataHoraFim), "kk:ss")}
          </p>
          <p>Local: {event.local.toString()}</p>
          <p>Descrição: {event.descricao.toString()}</p>
          <p>Vagas: {event.limiteVagas.toString()}</p>
          <UserPost id={id}></UserPost>
          <Button variant="contained" color="primary"><a href={url}>Lista de participantes</a></Button>
          
          
        </div>
      </div>
    );
  } else {
    return <div>um momento, por favor</div>;
  }
}

export default EventDetails;
