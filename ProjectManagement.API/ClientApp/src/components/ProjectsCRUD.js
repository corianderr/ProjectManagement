import React, {useEffect, useState} from 'react';
import {Link} from "react-router-dom";
import axios from "axios";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import {ToastContainer, toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'

const ProjectsCRUD = () => {
    const [loading, setLoading] = useState(true)
    const [data, setData] = useState([]);
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    
    const minPriority = 0;
    const maxPriority = 3;
    
    const [name, setName] = useState('');
    const [clientCompanyName, setClientCompanyName] = useState('');
    const [executorCompanyName, setExecutorCompanyName] = useState('');
    const [priority, setPriority] = useState(0);
    const [managerId, setManagerId] = useState(0);
    
    
    useEffect(() => {
        try {
            fetchData().catch((e) => console.log())
        } catch (e) {
            console.log(e.message)
            setLoading(false)
        }
    }, [])

    const fetchData = async () => {
        const {data} = await axios('api/projects');
        setData(data.result)
        setLoading(false)
    }
    
    const handleEdit = (id) => {
        //handleShow();
    }

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this project?")) {
            axios.delete(`api/projects/${id}`)
                .then((result) => {
                    if (result.status === 200){
                        toast.success('Project has been deleted');
                        fetchData();
                    }
                })
                .catch((error) => {
                    toast.error(error)
                })
        }
    }

    const handleAdd = (e) => {
        e.preventDefault()
        const url = 'api/projects';
        const createModel = {
            "name": name,
            "clientCompanyName": clientCompanyName,
            "executorCompanyName": executorCompanyName,
            "priority": priority,
            "managerId": managerId
        }
        console.log(createModel)
        
        axios.post(url, createModel)
            .then((result) => {
                console.log(result)
                fetchData().catch((e) => console.log())
                handleClose()
                clear()
                toast.success('Project has been added');
            }).catch(error => {
            console.log(error.response.data.errors)
        })
    }
    
    const clear = () => {
        setName('')
        setClientCompanyName('')
        setExecutorCompanyName('')
        setPriority(0)
        setManagerId(0)
    }

    return (
        <div>
            <ToastContainer/>
            {
                loading ? <p><em>Loading...</em></p> :
                    <div>
                        <Form>
                            <Row>
                                <Col>
                                    <Form.Label>Name</Form.Label>
                                    <Form.Control placeholder="Peter" />
                                </Col>
                                <Col>
                                    <Form.Label>Priority</Form.Label>
                                    <Form.Control placeholder="1" type="number" min={minPriority} max={maxPriority}/>
                                </Col>
                                <Col>
                                    <Form.Label>Start Date From</Form.Label>
                                    <Form.Control type="date"/>
                                </Col>
                                <Col>
                                    <Form.Label>Start Date To</Form.Label>
                                    <Form.Control type="date"/>
                                </Col>
                                <Col>
                                    <Button className="ms-auto w-100" variant="none" onClick={handleShow}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60"
                                             fill="currentColor" className="bi bi-plus-circle-fill" viewBox="0 0 16 16">
                                            <path
                                                d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8.5 4.5a.5.5 0 0 0-1 0v3h-3a.5.5 0 0 0 0 1h3v3a.5.5 0 0 0 1 0v-3h3a.5.5 0 0 0 0-1h-3v-3z"/>
                                        </svg></Button>
                                </Col>
                            </Row>
                        </Form>
                <table className='table table-striped text-center' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Actions</th>
                    </tr>
                    </thead>
                    <tbody>
                    {data.map((project, index) => <tr key={index}>
                        <td>{project.id}</td>
                        <td><Link tag={Link} className="text-dark"
                                  to={`/getById/${project.id}`}>{project.name}</Link></td>
                        <td>
                            <button className="btn btn-primary px-4 me-2" onClick={() => handleEdit(project.id)}>Edit</button>
                            <button className="btn btn-danger px-4 ms-2" onClick={() => handleDelete(project.id)}>Delete</button>
                        </td>
                    </tr>)}
                    </tbody>
                </table>
                    </div>
            }
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Project</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={(e) => handleAdd(e)}>
                        <div className="pb-3">
                            <input type="text" className="form-control" placeholder="Enter name" 
                                   onChange={(e) => setName(e.target.value)} required/>
                        </div>
                        <div className="pb-3">
                            <input type="text" className="form-control" placeholder="Enter client's company name" 
                                   onChange={(e) => setClientCompanyName(e.target.value)} required/>
                        </div>
                        <div className="pb-3">
                            <input type="text" className="form-control" placeholder="Enter executor's company name"
                                   onChange={(e) => setExecutorCompanyName(e.target.value)} required/>
                        </div>
                        <div className="pb-3">
                            <input type="number" min={0} max={3} className="form-control" placeholder="Enter priority"
                                   onChange={(e) => setPriority(parseInt(e.target.value))} required/>
                        </div>
                        <div className="pb-3">
                            <input type="number" min={0} max={3} className="form-control" placeholder="Choose manager id"
                                   onChange={(e) => setManagerId(parseInt(e.target.value))} required/>
                        </div>
                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                </Modal.Body>
            </Modal>
        </div>);
}

export default ProjectsCRUD;