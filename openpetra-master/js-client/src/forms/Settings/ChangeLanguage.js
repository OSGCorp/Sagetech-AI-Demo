// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       Timotheus Pokorra <timotheus.pokorra@solidcharity.com>
//
// Copyright 2017-2018 by TBits.net
// Copyright 2020 by SolidCharity.com
//
// This file is part of OpenPetra.
//
// OpenPetra is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.  If not, see <http://www.gnu.org/licenses/>.
//

$("#btnEnglish").click(function(e) {
	e.preventDefault();
	i18next.changeLanguage("en");
	nav.OpenForm("Home", "home");
        location.reload();
});

$("#btnGerman").click(function(e) {
	e.preventDefault();
	i18next.changeLanguage("de");
	nav.OpenForm("Home", "home");
        location.reload();
});

$("#btnNorwegian").click(function(e) {
	e.preventDefault();
	i18next.changeLanguage("nb-NO");
	nav.OpenForm("Home", "home");
        location.reload();
});
