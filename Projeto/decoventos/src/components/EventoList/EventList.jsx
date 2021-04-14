import {Button} from "@material-ui/core";
import React, {useState} from "react";
import {listEvent} from "../core/useDecoventosControl";
import "./EventList.css";
import EventListUi from "./EventListUi";

function EventList() {
  const [apiList, setApiList] = useState(null);

  React.useEffect(() => {
    listEvent().then((r) => setApiList(r));
  }, []);

  if (apiList) {
    return (
      <section className="list-main">
        
            <EventListUi apiList={apiList}/>
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

export default EventList;
