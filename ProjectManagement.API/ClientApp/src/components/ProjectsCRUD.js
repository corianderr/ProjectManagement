import React, {useEffect, useState} from 'react';
import {Link} from "react-router-dom";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import {toast, ToastContainer} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import Confetti from './Confetti';
import axiosApi from "../axiosApi";

const ProjectsCRUD = () => {
    const [loading, setLoading] = useState(true);
    const [data, setData] = useState([]);
    const [show, setShow] = useState(false);
    const [editShow, setEditShow] = useState(false);
    const [executorsShow, setExecutorsShow] = useState(false);
    const [addExecutor, setAddExecutor] = useState({
        projectId: 0,
        employeeId: 0
    });
    const [form, setForm] = useState({
        name: "",
        clientCompanyName: "",
        executorCompanyName: "",
        priority: 0,
        managerId: 0
    });
    const [filter, setFilter] = useState({
        name: "",
        priority: "",
        startDateFrom: null,
        startDateTo: null,
        orderBy: ""
    });
    const [sort, setSort] = useState({
        name: true,
        priority: true,
        startDate: true,
    });
    const [employees, setEmployees] = useState([]);
    const [isVisible, setIsVisible] = useState(false);

    const handleClose = () => {
        setShow(false);
        clear();
    };

    const handleEditClose = () => {
        setEditShow(false);
        clear();
    };

    const handleExecutorsClose = () => {
        setExecutorsShow(false);
        setAddExecutor({
            projectId: 0,
            employeeId: 0
        });
    }

    useEffect(() => {
        try {
            fetchData().catch((e) => console.log());
        } catch (error) {
            toast.error(error);
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        handleFilter().catch(() => console.log());
    }, [filter]);

    const handleShow = () => {
        setShow(true);
        setIsVisible(false);
        getEmployees().catch((e) => console.log());
    };

    const handleEditShow = async (id) => {
        const result = await axiosApi.get(`projects/getById/${id}`);
        await getEmployees();
        setIsVisible(false);
        setForm(result.data.result);
        setEditShow(true);
    };

    const handleExecutorsShow = async (id) => {
        await getEmployees();
        setExecutorsShow(true);
        setAddExecutor(prev => ({...prev, projectId: id}));
    };

    const handleAddExecutor = async (e) => {
        e.preventDefault();
        await axiosApi.put(`projects/addEmployee/${addExecutor.projectId}/${addExecutor.employeeId}`);
        handleExecutorsClose();
        toast.success("Executor was successfully added");
    }

    const onChangeExecutor = e => {
        const {name, value} = e.target;
        setAddExecutor(prev => ({...prev, [name]: value}));
    };

    const getEmployees = async () => {
        const result = await axiosApi.get('employees');
        setEmployees(result.data.result.map(item => {
            return item.id
        }));
    };

    const fetchData = async () => {
        const {data} = await axiosApi.get('projects');
        setData(data.result);
        setLoading(false);
    };

    const handleEdit = async (e, id) => {
        e.preventDefault();
        await axiosApi.put(`projects/${id}`, form);
        await fetchData();
        handleEditClose();
        toast.success('Project has been updated');
        setIsVisible(true);
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this project?")) {
            await axiosApi.delete(`projects/${id}`);
            toast.success('Project has been deleted');
            await fetchData();
        }
    };

    const onChange = e => {
        const {name, value} = e.target;
        setForm(prev => ({...prev, [name]: value}));
    };

    const onChangeFilter = e => {
        const {name, value} = e.target;
        setFilter(prev => ({...prev, [name]: value}));
    };

    const handleAdd = async (e) => {
        e.preventDefault();
        await axiosApi.post('projects', form);
        await fetchData();
        handleClose();
        toast.success('Project has been added');
        setIsVisible(true);
    };

    const handleFilter = async () => {
        const dataFrom = filter.startDateFrom == null ? "" : `&startDateFrom=${filter.startDateFrom}`;
        const dataTo = filter.startDateTo == null ? "" : `&startDateTo=${filter.startDateTo}`;
        const priority = filter.priority === "" ? "" : `&priority=${filter.priority}`;
        const url = `projects?name=${filter.name}` + priority + dataFrom + dataTo + `&orderBy=${filter.orderBy}`;
        const result = await axiosApi.get(url);
        setData(result.data.result);
    };

    const handleSort = (name, value) => {
        setSort(prev => ({...prev, [name]: !value}));
        const sortState = name + (value ? "Asc" : "Desc");
        setFilter(prev => ({...prev, orderBy: sortState}));
    };

    const clear = () => {
        setForm({name: "", clientCompanyName: "", executorCompanyName: "", priority: 0, managerId: 0})
    }

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
                                <Form.Control value={filter.name} name="name" onChange={onChangeFilter}/>
                            </Col>
                            <Col>
                                <Form.Label>Priority</Form.Label>
                                <Form.Control name="priority" type="number"
                                              min={0} max={3} onChange={onChangeFilter}/>
                            </Col>
                            <Col>
                                <Form.Label>Start Date From</Form.Label>
                                <Form.Control name="startDateFrom" type="date"
                                              onChange={onChangeFilter}/>
                                {isVisible && <Confetti/>}
                            </Col>
                            <Col>
                                <Form.Label>Start Date To</Form.Label>
                                <Form.Control name="startDateTo" type="date"
                                              onChange={onChangeFilter}/>
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
                                <th onClick={() => handleSort("name", sort.name)}>Name {getArrow(sort.name)}</th>
                                <th onClick={() => handleSort("priority", sort.priority)}>Priority {getArrow(sort.priority)}</th>
                                <th onClick={() => handleSort("startDate", sort.startDate)}>Start
                                    Date {getArrow(sort.startDate)}</th>
                                <th>Actions</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((project, index) => <tr key={index}>
                                <td>{project.id}</td>
                                <td><Link tag={Link} className="text-dark"
                                          to={`/projects/getById/${project.id}`}>{project.name}</Link></td>
                                <td>{project.priority}</td>
                                <td>{project.startDate}</td>
                                <td>
                                    <div className="btn-group">
                                        <button className="btn btn-secondary px-4"
                                                onClick={() => handleExecutorsShow(project.id)}>Add executor
                                        </button>
                                        <button className="btn btn-primary px-4"
                                                onClick={() => handleEditShow(project.id)}>Edit
                                        </button>
                                        <button className="btn btn-danger px-4"
                                                onClick={() => handleDelete(project.id)}>Delete
                                        </button>
                                    </div>
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
                    {
                        employees.length > 0 ?
                            <form onSubmit={(e) => handleAdd(e)}>
                                <div className="pb-3">
                                    <input value={form.name} name="name" type="text" className="form-control"
                                           placeholder="Enter name" onChange={onChange} required/>
                                </div>
                                <div className="pb-3">
                                    <input value={form.clientCompanyName} name="clientCompanyName" type="text"
                                           className="form-control"
                                           placeholder="Enter client's company name" onChange={onChange} required/>
                                </div>
                                <div className="pb-3">
                                    <input value={form.executorCompanyName} name="executorCompanyName" type="text"
                                           className="form-control" placeholder="Enter executor's company name"
                                           onChange={onChange} required/>
                                </div>
                                <div className="pb-3">
                                    <label htmlFor="priorityAdd" className="ps-2">Priority</label>
                                    <input value={form.priority} name="priority" id="priorityAdd" type="number" min={0}
                                           max={3}
                                           className="form-control" placeholder="Enter priority" onChange={onChange}
                                           required/>
                                </div>
                                <div className="pb-3">
                                    <label htmlFor="managerAdd" className="ps-2">Manager Id</label>
                                    <Form.Select id="managerAdd" name="managerId" onChange={onChange}>{
                                        employees.map((id) =>
                                            <option key={id} value={id}>{id}</option>)
                                    }</Form.Select>
                                </div>
                                <button type="submit" className="btn btn-primary">Submit</button>
                            </form>
                            : <p>No employees yet. Go and create one: <Link tag={Link} className="text-dark d-inline"
                                                                            to="/employees">here</Link></p>
                    }
                </Modal.Body>
            </Modal>
            <Modal show={editShow} onHide={handleEditClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Project</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={(e) => handleEdit(e, form.id)}>
                        <div className="pb-3">
                            <input value={form.name} name="name" type="text" className="form-control"
                                   placeholder="Enter name" onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.clientCompanyName} name="clientCompanyName" type="text"
                                   className="form-control"
                                   placeholder="Enter client's company name" onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.executorCompanyName} name="executorCompanyName" type="text"
                                   className="form-control" placeholder="Enter executor's company name"
                                   onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            <input value={form.priority} name="priority" type="number" min={0} max={3}
                                   className="form-control" placeholder="Enter priority" onChange={onChange} required/>
                        </div>
                        <div className="pb-3">
                            {
                                employees != null ?
                                    <Form.Select name="managerId" onChange={onChange}>{
                                        employees.map((id) =>
                                            <option key={id} value={id}>{id}</option>)
                                    }</Form.Select>
                                    : <p></p>
                            }
                        </div>
                        <button type="submit" className="btn btn-primary">Submit</button>
                    </form>
                </Modal.Body>
            </Modal>
            <Modal show={executorsShow} onHide={handleExecutorsClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add executor to project #{addExecutor.projectId}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={(e) => handleAddExecutor(e)}>
                        <div className="pb-3">
                            <Form.Label htmlFor="executorId">Choose executor's id</Form.Label>
                            {
                                employees != null ?
                                    <Form.Select id="executorId" name="employeeId" onChange={onChangeExecutor}>{
                                        employees.map((id) =>
                                            <option key={id} value={id}>{id}</option>)
                                    }</Form.Select>
                                    : <p></p>
                            }
                        </div>
                        <button type="submit" className="btn btn-primary">Add</button>
                    </form>
                </Modal.Body>
            </Modal>
        </div>);
};

export default ProjectsCRUD;