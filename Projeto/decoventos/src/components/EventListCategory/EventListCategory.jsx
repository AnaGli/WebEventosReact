import { Button, Typography } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { listCategories } from "../core/useDecoventosControl";
import "./EventListCategory.css";

function EventListCategory() {
  const [apiList, setApiList] = useState(null);

  useEffect(() => {
    listCategories().then((r) => setApiList(r));
    
  }, []);
  

  const listaImagens = [
    'https://image.freepik.com/vetores-gratis/ilustracao-de-conceito-de-atividade-de-desenvolvedor_114360-2801.jpg',
    'https://image.freepik.com/vetores-gratis/ilustracao-do-conceito-de-programacao_114360-1351.jpg',
    'https://image.freepik.com/vetores-gratis/conceito-de-telefone-movel-de-marketing-de-midia-social_23-2148420355.jpg',
    'https://image.freepik.com/vetores-gratis/ilustracao-em-vetor-conceito-abstrato-de-equipe-devops-membro-da-equipe-de-desenvolvimento-de-software-fluxo-de-trabalho-agil-modelo-de-equipe-devops-trabalho-em-equipe-de-ti-gerenciamento-de-projetos-metafora-abstrata-de-pratica-integrada_335657-2299.jpg',
    'https://image.freepik.com/vetores-gratis/futuro-do-espaco-de-trabalho-com-visualizacao-de-relatorio-de-dados-de-exibicao-grande-com-ar-com-estilo-isometrico_197170-71.jpg',
    'https://image.freepik.com/vetores-gratis/conceito-ilustrado-de-desenvolvimento-de-aplicativos_23-2148690655.jpg',
    'https://image.freepik.com/vetores-gratis/homem-olha-grafico-grafico-conceito-de-analise-de-negocios-grande-icone-de-processamento-de-dados_39422-761.jpg',
    'https://image.freepik.com/vetores-gratis/ciclo-de-vida-do-projeto-de-desenvolvimento-agil_115739-1010.jpg'
  ]

  if (apiList) {
    return (
      <section className="teste">
         <Typography variant="h5" component="h4" gutterBottom>
         Explore os eventos por categorias
          
          </Typography>
       
        {apiList.map((item, i) => {
          const url = "/evento-categoria/" + item.id.toString();
          return (
            <div className="list-categoria" key={i}>
                <nav >          
                  <img className="categoria-img" src={listaImagens[i]} alt=""/>
                  <p>{item.nomeCategoria}</p>
                  <Button variant="contained" color="primary"><a href={url}>Explorar</a></Button>
                </nav>

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

export default EventListCategory;
