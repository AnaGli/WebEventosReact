import React, { useState } from "react";
import { TextField, Button, Select, MenuItem } from "@material-ui/core";
import "./EventPost.css";
import { createEvent } from "../core/useDecoventosControl";

function EventPost() {
  const [nome, setNome] = useState("");
  const [dataHoraInicio, setDataInicio] = useState("");
  const [dataHoraFim, setDataFim] = useState("");
  const [local, setLocal] = useState("");
  const [descricao, setDescricao] = useState("");
  const [limiteVagas, setVagas] = useState("");
  const [idCategoriaEvento, setIdCategoriaEvento] = useState("");

  return (
    <form
      className="form-evento"
      noValidate
      autoComplete="off"
      onSubmit={(event) => {
        event.preventDefault();
        createEvent(
          nome,
          dataHoraInicio,
          dataHoraFim,
          local,
          descricao,
          limiteVagas,
          idCategoriaEvento
        );
        setNome("");
        setDataInicio("");
        setDataFim("");
        setLocal("");
        setDescricao("");
        setVagas("");
        setIdCategoriaEvento("");
      }}
    >
      <TextField
        
        value={nome}
        onChange={(event) => {
          setNome(event.target.value);
        }}
        fullWidth
        margin="normal"
        className="input"
        id="nome"
        label="Nome do Evento"
        variant="outlined"
      />
      <TextField
        value={dataHoraInicio}
        onChange={(event) => {
          setDataInicio(event.target.value);
        }}
        fullWidth
        margin="normal"
        variant="outlined"
        id="dataHoraInicio"
        label="Data do evento e horário de inicío"
        type="datetime-local"
        InputLabelProps={{
          shrink: true,
        }}
      />
      <TextField
        value={dataHoraFim}
        onChange={(event) => {
          setDataFim(event.target.value);
        }}
        fullWidth
        margin="normal"
        variant="outlined"
        id="dataHoraFim"
        label="Data do evento e horário de encerramento"
        type="datetime-local"
        InputLabelProps={{
          shrink: true,
        }}
      />
      <TextField
        value={local}
        onChange={(event) => {
          setLocal(event.target.value);
        }}
        fullWidth
        margin="normal"
        id="local"
        label="Local do Evento"
        variant="outlined"
      />
      <TextField
        value={descricao}
        onChange={(event) => {
          setDescricao(event.target.value);
        }}
        fullWidth
        margin="normal"
        id="descricao"
        label="Descrição"
        variant="outlined"
      />
      <TextField
        value={limiteVagas}
        onChange={(event) => {
          setVagas(event.target.value);
        }}
        fullWidth
        margin="normal"
        type="number"
        id="limiteVagas"
        label="Vagas"
        variant="outlined"
      />

      <Select
        fullWidth
        variant="outlined"
        label="Categoria do Evento"
        value={idCategoriaEvento}
        onChange={(event) => {
          setIdCategoriaEvento(event.target.value);
        }}
      >
        <MenuItem value="">Categoria do Evento</MenuItem>
        <MenuItem value={1}>Backend</MenuItem>
        <MenuItem value={2}>Frontend</MenuItem>
        <MenuItem value={3}>Mobile</MenuItem>
        <MenuItem value={4}>Cloud & DevOps</MenuItem>
        <MenuItem value={5}>Modern Workplaces</MenuItem>
        <MenuItem value={6}>UI/UX</MenuItem>
        <MenuItem value={7}>Data & Analytics</MenuItem>
        <MenuItem value={8}>Agilidade & Qualidade</MenuItem>
      </Select>
      
      <Button type="submit" variant="contained" color="primary">
        Cadastrar
      </Button>
    </form>
  );
}

export default EventPost;
