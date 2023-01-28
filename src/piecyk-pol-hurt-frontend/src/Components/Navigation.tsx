import {
  AppBar,
  Container,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Divider,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Drawer,
  Button,
  Badge,
  BadgeProps,
  styled,
} from "@mui/material";
import { useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import MenuIcon from "@mui/icons-material/Menu";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";

const StyledBadge = styled(Badge)<BadgeProps>(({ theme }) => ({
  '& .MuiBadge-badge': {
    right: -3,
    top: 13,
    border: `2px solid ${theme.palette.background.paper}`,
    padding: '0 4px',
  },
}));

const Navigation = () => {
  const pages = ["Produkty", "Zamówienia", "Lokacje"];
  const links = ["products", "orders", "locations"];
  const [drawerOpen, setDrawerOpen] = useState(false);
  const navigate = useNavigate();

  const handleDrawerToggle = () => {
    setDrawerOpen(!drawerOpen);
  };

  const { isAuthenticated, logout, loginWithPopup } = useAuth0();

  const authenticateAction = () => {
    if (isAuthenticated) {
      logout();
    } else {
      loginWithPopup({ ui_locales: "pl" });
    }
  };

  const drawer = (
    <Box onClick={handleDrawerToggle} sx={{ textAlign: "center" }}>
      <Typography variant="h6" sx={{ my: 2 }}>
        Piecyk Pol Hurt
      </Typography>
      <Divider />
      <List>
        {pages.map((item, index) => (
          <ListItem key={item} disablePadding>
            <ListItemButton sx={{ textAlign: "center" }}>
              <ListItemText
                primary={item}
                onClick={() => navigate(links[index])}
              />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Box>
  );

  return (
    <>
      <AppBar position="static">
        <Container
          maxWidth="xl"
          sx={{ height: "65x", marginLeft: 0, maxWidth: "3000px" }}
        >
          <Toolbar disableGutters>
            <Typography
              variant="h6"
              noWrap
              component="a"
              href="/"
              sx={{
                mr: 2,
                display: { xs: "none", md: "flex" },
                fontFamily: "monospace",
                fontWeight: 700,
                letterSpacing: ".3rem",
                color: "inherit",
                textDecoration: "none",
              }}
            >
              Piecyk Pol Hurt
            </Typography>

            <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
              <IconButton
                size="large"
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                onClick={(e) => handleDrawerToggle()}
                color="inherit"
              >
                {!drawerOpen ? <MenuIcon /> : <CloseIcon />}
              </IconButton>
            </Box>

            <Typography
              variant="h5"
              component="a"
              href=""
              sx={{
                mr: 2,
                display: { xs: "flex", md: "none" },
                flexGrow: 1,
                fontFamily: "monospace",
                fontWeight: 700,
                letterSpacing: ".3rem",
                color: "inherit",
                textDecoration: "none",
              }}
            >
              Piecyk Pol Hurt
            </Typography>
            <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
              {pages.map((page, index) => (
                <Button
                  key={page}
                  onClick={() => navigate(links[index])}
                  sx={{ my: 2, color: "white", display: "block" }}
                >
                  {page}
                </Button>
              ))}
            </Box>
            <IconButton size="large" sx={{mr: 2}}>
              <StyledBadge badgeContent={4} color="info">
                <ShoppingCartIcon sx={{ color: "white" }} />
              </StyledBadge>
            </IconButton>
            <Box sx={{ flexGrow: 0 }} onClick={authenticateAction}>
              {isAuthenticated ? "Wyloguj" : "Zaloguj"}
            </Box>
          </Toolbar>
        </Container>
      </AppBar>

      <Box component="nav">
        <Drawer
          variant="temporary"
          open={drawerOpen}
          onClose={handleDrawerToggle}
          ModalProps={{
            keepMounted: true,
          }}
          sx={{
            "& .MuiDrawer-paper": { boxSizing: "border-box", width: 300 },
          }}
        >
          {drawer}
        </Drawer>
      </Box>
    </>
  );
};

export default Navigation;
