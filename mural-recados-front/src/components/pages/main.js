import '../../App.css';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import BootstrapTable from 'react-bootstrap-table-next';
import { Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'

function Main() {
  const [nome, setNome] = useState('');
  const [recados, setRecados] = useState([]);
  const [recado, setRecado] = useState(null);
  const [idUsuario, setIdUsuario] = useState(null);
  const [disableName, setDisableName] = useState(false);

  useEffect(() => {
    getRecados();
  }, [])

  const onChangeName = (e) => {
    setNome(e.target.value);
  }

  const onChangeRecado = (e) => {
    setRecado(e.target.value);
  }

  const onClickEnviar = async () => {
    const usuario = {
      "apelido": nome
  }

    await axios.put(`https://localhost:49155/api/MuralRecado/PutUsuario`, usuario)
    .then(res => {
      setIdUsuario(res.data[0]);
      console.log(res);
      console.log(res.data);
    })
    setDisableName(true);
    
  }

  const getRecados = async () => {
    await axios.get(`https://localhost:49155/api/MuralRecado/GetRecados`)
    .then(res => {
      setRecados(res.data);
      console.log(res);
      console.log(res.data);
    })
  }

  const onClickEnviarRecado = async () => {
    const model = {
      texto: recado,
      usuarioCadastro: idUsuario,
      apelidoUsuario: nome

    }

    await axios.put(`https://localhost:49155/api/MuralRecado/PutRecado`, model)
    .then(res => {
      setRecados(res.data);
      console.log(res);
      console.log(res.data);
    }).finally(() => {
      window.location.reload();
    }); 
  }

  const onClickRemoverRecado = async (idRecado) => {
    debugger;
    const model = {
      recadoId: idRecado,

    }

    await axios.delete(`https://localhost:49155/api/MuralRecado/DeleteRecado`, { data: model })
    .then(res => {
      console.log(res);
      console.log(res.data);
    }).finally(() => {
      window.location.reload();
    }); 

  }

  const cellButtonRemover = (cell, row, rowIndex) => {
    return (
        <div>
            <Button type="button" style={{backgroundColor: '#a10000'}} onClick={ () => onClickRemoverRecado(row.recadoId)}>Remover Recado</Button>
        </div>
        );
}

  const columns = [{
    dataField: 'recadoId',
    text: 'Código'
  }, {
    dataField: 'apelidoUsuario',
    text: 'Apelido'
  }, 
  {
    dataField: 'usuarioCadastro',
    text: 'IdUsuario'
  },
  {
    dataField: 'texto',
    text: 'Recado'
  },
  {
    dataField: "button",
    formatter: cellButtonRemover,
    text: 'Remover Recado'
  }];

  return (
    <div className="App">
      <div className="center">
        <h1>Mural de recados</h1>
        <div className="row">
          <div className="col-md-2 center">
            <input type="text" className="form-control" onChange={onChangeName} disabled={disableName}  placeholder="Apelido" aria-label="Apelido" aria-describedby="basic-addon1" />
            <Button type="button" onClick={onClickEnviar}>Enviar</Button>
          </div>
        </div>

        <h3>Escreva um recado</h3>
        <div className="row">
            <div className="col-md-2 center">
              <input type="text" className="form-control" onChange={onChangeRecado} placeholder="Recado" aria-label="Recado" aria-describedby="basic-addon1" />
              <Button type="button" onClick={onClickEnviarRecado}>Enviar Recado</Button>
            </div>
        </div>

        <div className="row">
          <div className="col-md-8 center">
            <BootstrapTable keyField='id' data={recados} columns={ columns } 
              striped
              hover
              condensed
              noDataIndication="Nenhum recado encontrado"
            />
          </div>
        </div>
      </div>
    </div>

  );
}

export default Main;