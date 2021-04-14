import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { listParticipantByEvent } from "../core/useDecoventosControl";


function EventListParticipante() {
    let id = useParams();
    console.log(id);

    const [apiList, setApiList] = useState(null);
    useEffect(() => {
    listParticipantByEvent(id.id).then((r) => setApiList(r));
  }, [id]);
  console.log(apiList);
  console.log(id);
 
  if (apiList){

      return (
        <section className="teste">
            <h4> Participantes inscritos: {apiList.length}</h4>

          {apiList.map((item, i) => {
            return (
              <div className="list-categoria" key={i}>
                <nav>
                    
                  <p>{item.loginParticipante}</p>
                </nav>
              </div>
            );
          })}
        </section>
      );
  }
  else {
      return (
          <p>Carregando...</p>
      )
  }
  
  
  
}
export default EventListParticipante;
