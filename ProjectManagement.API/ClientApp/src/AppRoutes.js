import {Home} from "./components/Home";
import ProjectDetails from "./components/ProjectDetails";
import ProjectsCRUD from "./components/ProjectsCRUD";
import EmployeesCRUD from "./components/EmployeesCRUD";

const AppRoutes = [
    {
        index: true,
        element: <Home/>
    },
    {
        path: '/projects',
        element: <ProjectsCRUD/>
    },
    {
        path: '/projects/getById/:id',
        element: <ProjectDetails/>
    },
    {
        path: '/employees',
        element: <EmployeesCRUD/>
    }
];

export default AppRoutes;
