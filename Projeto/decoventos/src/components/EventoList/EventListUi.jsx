import { Button, Typography } from "@material-ui/core";
import "./EventList.css";
import "fontsource-roboto";
import Typing from "./Type";

function EventListUi(props) {
  if (props.apiList) {
    return (
      <section className="list-main">
        <div className="top">
          <Typography variant="h4" component="h4" gutterBottom>
            <h1>Decoventos</h1> 
            <h1>Eventos de <Typing/></h1> 
            
          
          </Typography>
        </div>

        {props.apiList.map((item, i) => {
          const url = "/evento/" + item.idEvento.toString();
          return (
            <div className="card">
              <div>
                <nav>
                  <h3>Evento: {item.nome}</h3>
                </nav>
                <h4>Categoria: {item.categoriaResponseModel.nomeCategoria}</h4>
                <h5>Descrição: {item.descricao}</h5>
                <Button variant="contained" color="primary">
                  <a href={url}>Saiba mais e cadastre-se</a>
                </Button>
              </div>
            </div>
          );
        })}
      </section>
    );
  } else {
    return (
      <div>
        <p> carregando... </p>
      </div>
    );
  }
}

export default EventListUi;
