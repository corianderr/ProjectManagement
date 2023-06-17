import React, {useCallback, useEffect, useState} from 'react';
import {toast, ToastContainer} from "react-toastify";
import axios from "axios";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Confetti from "./Confetti";
import Button from "react-bootstrap/Button";
import {Link} from "react-router-dom";
import Modal from "react-bootstrap/Modal";
import axiosApi from "../axiosApi";

const EmployeesCrud = () => {
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState([]);
    const [isVisible, setIsVisible] = useState(false);
    const [show, setShow] = useState(false);
    const [editShow, setEditShow] = useState(false);
    const [filter, setFilter] = useState({
        name: "",
        surname: "",
        email: "",
        orderBy: ""
    });
    const [sort, setSort] = useState({
        name: true,
        surname: true,
        email: true,
    });
    const [form, setForm] = useState({
        name: "",
        surname: "",
        patronymic: "",
        email: ""
    });

    useEffect(() => {
        try {
            fetchData().catch((e) => console.log())
        } catch (error) {
            toast.error(error)
            setLoading(false)
        }
    }, []);

    useEffect(() => {
        handleFilter().catch(() => console.log());
    }, [filter]);

    const handleClose = () => {
        setShow(false);
        clear();
    };

    const handleEditClose = () => {
        setEditShow(false);
        clear();
    };

    const handleShow = () => {
        setShow(true);
        setIsVisible(false);
    };

    const handleEditShow = async (id) => {
        const result = await axiosApi.get(`employees/getById/${id}`);
        setForm(result.data.result);
        setEditShow(true);
        setIsVisible(false);
    };

    const onChange = e => {
        const {name, value} = e.target;
        setForm(prev => ({...prev, [name]: value}));
    };

    const clear = () => {
        setForm({name: "", surname: "", patronymic: "", email: ""});
    };

    const fetchData = async () => {
        const {data} = await axiosApi.get('employees');
        setData(data.result);
        setLoading(false);
    };

    const handleAdd = async (e) => {
        e.preventDefault();
        await axiosApi.post('employees', form);
        await fetchData();
        handleClose();
        toast.success('Employee has been added');
        setIsVisible(true);
    };

    const handleEdit = async (e, id) => {
        e.preventDefault();
        await axiosApi.put(`employees/${id}`, form);
        await fetchData();
        handleEditClose();
        toast.success('Employee has been updated');
        setIsVisible(true);
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this employee?")) {
            await axiosApi.delete(`employees/${id}`);
            toast.success('Employee has been deleted');
            await fetchData();
        }
    };

    const onChangeFilter = e => {
        const {name, value} = e.target;
        setFilter(prev => ({...prev, [name]: value}));
    };

    const handleFilter = async () => {
        const url = `employees?name=${filter.name}&surname=${filter.surname}&email=${filter.email}&orderBy=${filter.orderBy}`;
        const result = await axiosApi.get(url);
        setData(result.data.result);
    };

    const handleSort = (name, value) => {
        setSort(prev => ({...prev, [name]: !value}));
        const sortState = name + (value ? "Asc" : "Desc");
        setFilter(prev => ({...prev, orderBy: sortState}));
    };

    const getArrow = (condition) => {
        if (!condition) {
            return (<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                         className="bi bi-arrow-up" viewBox="0 0 16 16">
                <path fillRule="evenodd"
                      d="M8 15a.5.5 0 0 0 .5-.5V2.707l3.146 3.147a.5.5 0 0 0 .708-.708l-4-4a.5.5 0 0 0-.708 0l-4 4a.5.5 0 1 0 .708.708L7.5 2.707V14.5a.5.5 0 0 0 .5.5z"/>
            </svg>);
        }
        return (<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                     className="bi bi-arrow-down" viewBox="0 0 16 16">
            <path fillRule="evenodd"
                  d="M8 1a.5.5 0 0 1 .5.5v11.793l3.146-3.147a.5.5 0 0 1 .708.708l-4 4a.5.5 0 0 1-.708 0l-4-4a.5.5 0 0 1 .708-.708L7.5 13.293V1.5A.5.5 0 0 1 8 1z"/>
        </svg>);
    };

    return (
        <div>
            <ToastContainer/>
            {
                loading ? <p><em>Loading...</em></p> :
                    <div>
                        <Row>
                            <Col>
                                <Form.Label>Name</Form.Label>
                                <Form.Control name="name" type="text" onChange={onChangeFilter}/>
                            </Col>
                            <Col>
                                <Form.Label>Surname</Form.Label>
                                <Form.Control name="surname" type="text" onChange={onChangeFilter}/>
                            </Col>
                            <Col>
                                <Form.Label>Email</Form.Label>
                                <Form.Control name="email" type="text" onChange={onChangeFilter}/>
                                {isVisible && <Confetti/>}
                            </Col>
                            <Col>
                                <div className="h-100 d-flex">
                                    <Button className="mt-auto px-4" variant="secondary"
                                            onClick={(e) => handleFilter(e)}>Filter</Button>
                                </div>
                            </Col>
                            <Col>
                                <Button className="ms-auto w-100 opacity-25" variant="none" onClick={handleShow}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="60" height="60"
                                         fill="currentColor" className="bi bi-plus-circle-fill" viewBox="0 0 16 16">
                                        <path
                                            d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8.5 4.5a.5.5 0 0 0-1 0v3h-3a.5.5 0 0 0 0 1h3v3a.5.5 0 0 0 1 0v-3h3a.5.5 0 0 0 0-1h-3v-3z"/>
                                    </svg>
                                </Button>
                            </Col>
                        </Row>
                        <table className='table table-striped text-center' aria-labelledby="tabelLabel">
                            <thead>
                            <tr>
                                <th>Id</th>
                                <th onClick={() => handleSort("surname", sort.surname)}>Surname {getArrow(sort.surname)}</th>
                                <th onClick={() => handleSort("name", sort.name)}>Name {getArrow(sort.name)}</th>
                                <th>Patronymic</th>
                                <th onClick={() => handleSort("email", sort.email)}>Email {getArrow(sort.email)}</th>
                                <th>Actions</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((employee, index) => <tr key={index}>
                                <td>{employee.id}</td>
                                <td>{employee.surname}</td>
                                <td>{employee.name}</td>
                                <td>{employee.patronymic}</td>
                                <td>{employee.email}</td>
                                <td>
                                    <button className="btn btn-primary px-4 me-2"
                                            onClick={() => handleEditShow(employee.id)}>Edit
                                    </button>
                                    <button className="btn btn-danger px-4 ms-2"
                                            onClick={() => handleDelete(employee.id)}>Delete
                                    </button>
                                </td>
                            </tr>)}
                            </tbody>
                        </table>
                    </div>
            }
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Employee</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={(e) => handleAdd(e)}>
                        <div className="pb-3">
                            <input value={form.name} name="name" type="text" className="form-control"
                                   placeholder="Enter name" onChange={onChange}/>
                        </div>
                        <div className="pb-3">
                            <input value={form.surname} name="surname" type="text"
                                   className="form-control"
                                   placeholder="Enter surname" onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.patronymic} name="patronymic" type="text"
                                   className="form-control" placeholder="Enter patronymic"
                                   onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.email} name="email" type="text"
                                   className="form-control" placeholder="Enter email"
                                   onChange={onChange} required/>
                        </div>
                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                </Modal.Body>
            </Modal>
            <Modal show={editShow} onHide={handleEditClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Employee</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={(e) => handleEdit(e, form.id)}>
                        <div className="pb-3">
                            <input value={form.name} name="name" type="text" className="form-control"
                                   placeholder="Enter name" onChange={onChange}/>
                        </div>
                        <div className="pb-3">
                            <input value={form.surname} name="surname" type="text"
                                   className="form-control"
                                   placeholder="Enter surname" onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.patronymic} name="patronymic" type="text"
                                   className="form-control" placeholder="Enter patronymic"
                                   onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.email} name="email" type="text"
                                   className="form-control" placeholder="Enter email"
                                   onChange={onChange} required/>
                        </div>
                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                </Modal.Body>
            </Modal>
        </div>);
};

export default EmployeesCrud;