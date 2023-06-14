import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import ProjectDetails from "./components/ProjectDetails";
import ProjectsCRUD from "./components/ProjectsCRUD";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/projects',
    element: <ProjectsCRUD />
  },
  {
    path: '/getById/:id',
    element: <ProjectDetails/>
  }
];

export default AppRoutes;
