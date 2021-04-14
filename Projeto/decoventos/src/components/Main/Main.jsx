import React from "react";
import EventPost from "../EventPost";
import {
  BrowserRouter as Router,
  NavLink,
  Route,
  Switch,
} from "react-router-dom";
import "./Main.css";
import EventList from "../EventoList";
import EventListCategory from "../EventListCategory/EventListCategory";
import EventCategory from "../EventCategory/EventCategory";
import EventDetails from "../EventDetails";
import EventListParticipante from "../EventDetails/EventListParticipante";

function Main() {
  return (
    <Router>
      <div className="App">
        <nav className="head">
          <div>
            <NavLink style={{ textDecoration: "none" }} to="/" exact>
              <h1>Home</h1>
            </NavLink>
          </div>
          <div>
            <NavLink style={{ textDecoration: "none" }} to="/evento-categoria">
              <h1>Eventos por categoria</h1>
            </NavLink>
          </div>
          <div>
            <NavLink style={{ textDecoration: "none" }} to="/cadastro-evento">
              <h1>Cadastre novo evento</h1>
            </NavLink>
          </div>
        </nav>
        <header>
          <Switch>
            <Route path="/cadastro-evento">
              <EventPost />
            </Route>
            <Route path="/evento-categoria/:id">
              <EventCategory />
            </Route>
            <Route path="/evento-categoria">
              <EventListCategory />
            </Route>
            <Route path="/evento/:id">
              <EventDetails />
            </Route>
            <Route path="/participante/:id">
              <EventListParticipante />
            </Route>
            <Route path="/">
              <EventList />
            </Route>
          </Switch>
        </header>
      </div>
    </Router>
  );
}

export default Main;
