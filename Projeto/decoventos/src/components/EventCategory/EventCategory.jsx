import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { listEventByCategory } from "../core/useDecoventosControl";
import EventListUi from "../EventoList/EventListUi";

function EventCategory() {
  let { id } = useParams();

  const [apiList, setApiList] = useState(null);

  useEffect(() => {
    listEventByCategory(id).then((r) => {
      setApiList(r);
    });
  }, [id]);

  if (apiList) {
    return (
      <section>
        <EventListUi apiList={apiList} />
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

export default EventCategory;
