import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import { userSignup } from "../core/useDecoventosControl";

export default function UserPost(props) {
  const [open, setOpen] = React.useState(false);
  const [loginParticipante, setLogin] = useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };

  function singUp() {
    userSignup(props.id, loginParticipante);
    handleClose();
    setLogin("");
  }

  return (
    <div>
      <Button variant="outlined" color="primary" onClick={handleClickOpen}>
        Cadastre-se
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">Inscrição</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Para se inscrever, digite seu nome:
          </DialogContentText>
          <TextField
            value={loginParticipante}
            onChange={(event) => {
              setLogin(event.target.value);
            }}
            autoFocus
            margin="dense"
            id="loginParticipante"
            label="Nome"
            type="text"
            fullWidth
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Cancelar
          </Button>
          <Button onClick={singUp} color="primary">
            Cadastre-se
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
